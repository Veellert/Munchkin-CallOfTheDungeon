using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику монстра
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AnimationCaller), typeof(DirectionStatementManager))]
public abstract class Monster : MonoBehaviour, IUnit, IDamager, IDamageable
{
    #region Static

    private static List<Monster> monsterList;

    /// <summary>
    /// Удаляет монстра из списка
    /// </summary>
    public static void RemoveMonsterFromStack(Monster monster)
    {
        if (monster && monsterList != null)
        {
            monsterList.Remove(monster);
            MonstersCount--;
        }
    }

    /// <summary>
    /// Добавляет монстра в список
    /// </summary>
    public static void AddMonsterToStack(Monster monster)
    {
        if (monsterList == null)
            monsterList = new List<Monster>();
        if (MonstersCount == null)
            MonstersCount = monsterList.Count;
        if (monster)
        {
            monsterList.Add(monster);

            if (monsterList.Count > MonstersCount.MaxValue)
                MonstersCount.SetMax(monsterList.Count);
            MonstersCount++;
        }
    }

    /// <summary>
    /// Возвращает всех монстров списка
    /// </summary>
    public static List<Monster> GetMonsters()
    {
        if (monsterList == null)
            monsterList = new List<Monster>();

        return monsterList;
    }

    /// <summary>
    /// Возвращает самого близкого монстра к точке
    /// </summary>
    /// <param name="position">Точка</param>
    /// <param name="ParamName">Радиус</param>
    public static Monster GetClosestMonster(Vector2 position, float range)
    {
        Monster closestMonster = null;

        if (monsterList != null)
            foreach (var monster in monsterList.FindAll(s => Vector2.Distance(position, s.transform.position) < range))
            {
                if (closestMonster == null)
                    closestMonster = monster;
                else
                {
                    if (Vector2.Distance(position, monster.transform.position) < Vector2.Distance(position, closestMonster.transform.position))
                        closestMonster = monster;
                }
            }

        return closestMonster;
    }

    /// <summary>
    /// Получает босса по его типу
    /// </summary>
    /// <param name="bossType">Тип босса (typeof)</param>
    public static Monster GetBoss(Type bossType)
    {
        return monsterList.Find(s => s.GetType() == bossType);
    }

    public static UnitAttrib MonstersCount { get; set; }

    #endregion

    protected DirectionStatementManager _directionManager;
    protected AnimationCaller _animation;

    #region Attrib

    [SerializeField] private string _unitName = "Безымянный монстр";
    public string UnitName { get => _unitName; set => _unitName = value; }
    [SerializeField] private UnitAttrib _speed = new UnitAttrib(1, 10);
    public UnitAttrib Speed { get => _speed; set => _speed = value; }

    [SerializeField] private int _level = 1;
    public int Level { get => _level; set => _level = value; }
    [SerializeField] private UnitAttrib _chaseRadius = new UnitAttrib(4, 10);
    public UnitAttrib ChaseRadius { get => _chaseRadius; set => _chaseRadius = value; }
    [SerializeField] private UnitAttrib _btwTargetDistance = 1.3f;
    public UnitAttrib BtwTargetDistance { get => _btwTargetDistance; set => _btwTargetDistance = value; }

    [SerializeField] private UnitAttrib _damage = new UnitAttrib(15, 100);
    public UnitAttrib Damage { get => _damage; set => _damage = value; }
    [SerializeField] private UnitAttrib _attackCooldown = 1;
    public UnitAttrib AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }

    [SerializeField] private UnitAttrib _hp = 100;
    public UnitAttrib HP { get => _hp; set => _hp = value; }
    public bool IsDead => HP.IsValueEmpty();

    #endregion

    protected eState _state = eState.Default;
    /// <summary>
    /// Состояние монстра
    /// </summary>
    protected enum eState
    {
        Disabled,
        Default,
        Chase,
        Attack,
        SpecialAttack,
        Die,
    }

    protected Vector2 _movementDirection;
    protected Transform _chaseTarget;

    protected virtual void Start()
    {
        _directionManager = GetComponent<DirectionStatementManager>();
        _animation = GetComponent<AnimationCaller>();

        name = UnitName;

        if(Player.Instance)
            _chaseTarget = Player.Instance.transform;

        AddMonsterToStack(this);
    }

    protected virtual void FixedUpdate()
    {
        if (Player.Instance)
            _chaseTarget = Player.Instance.transform;

        if (_chaseTarget == null)
            return;

        if (Vector2.Distance(transform.position, _chaseTarget.position) > new TileHalf(20))
        {
            if(_state != eState.Disabled)
            {
                _state = eState.Disabled;
                _animation.Disabled();
            }
        }
        else
        {
            if(_state == eState.Disabled)
                _state = eState.Default;
        }
    }

    private void OnDestroy()
    {
        RemoveMonsterFromStack(this);
    }

    /// <summary>
    /// Смерть монстра
    /// </summary>
    public virtual void Die()
    {
        _state = eState.Die;
        _animation.PlayDIE();
        GetComponent<Collider2D>().isTrigger = true;
        RemoveMonsterFromStack(this);
    }

    /// <summary>
    /// Получить урон
    /// </summary>
    public virtual void GetDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP.IsValueEmpty())
            Die();
    }

    /// <summary>
    /// Восстановить здоровье
    /// </summary>
    public virtual void Heal(float healAmount)
    {
        HP += healAmount;
    }

    /// <summary>
    /// Проверка на атаку
    /// </summary>
    public virtual void AttackInput()
    {
        if (AttackCooldown.IsValueEmpty())
        {
            if (_state != eState.Attack)
                _state = eState.Attack;

            AttackCooldown.FillToMax();
        }
    }

    /// <summary>
    /// Обработчик атаки
    /// </summary>
    public virtual void AttackHandler(float attackRange)
    {
        if (!AttackCooldown.IsValueEmpty())
            return;

        var attackDirection = (_chaseTarget.position - transform.position).normalized;
        var attackPosition = transform.position + attackDirection * new TileHalf(BtwTargetDistance);

        if (Vector2.Distance(Player.Instance.transform.position, transform.position) < new TileHalf(BtwTargetDistance))
            attackPosition = Player.Instance.transform.position;

        _ap = attackPosition;
        _ar = attackRange;

        _animation.PlayATTACK(() =>
        {
            if (Vector2.Distance(attackPosition, _chaseTarget.position) < attackRange)
                _chaseTarget.GetComponent<IDamageable>().GetDamage(Damage);

            TryChase();
        });
    }

    /// <summary>
    /// Проверка на кулдаун атаки
    /// </summary>
    public virtual void CheckAttackCooldown()
    {
        if (AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Обработчик преследования
    /// </summary>
    protected virtual void ChaseHandler()
    {
        TryChase();

        if (Vector2.Distance(transform.position, _chaseTarget.position) > new TileHalf(BtwTargetDistance))
            MoveTo(_chaseTarget.position, 1);
        else
        {
            _animation.PlayIDLE(Vector2.zero);
            AttackInput();
        }
    }

    /// <summary>
    /// Попытка преследования
    /// </summary>
    protected virtual void TryChase()
    {
        if (!Player.Instance)
            return;

        if (_chaseTarget == null)
            _chaseTarget = Player.Instance.transform;

        if (Vector2.Distance(transform.position, _chaseTarget.position) <= new TileHalf(ChaseRadius))
            if(!_chaseTarget.GetComponent<IDamageable>().IsDead)
            {
                SetDirection(_chaseTarget.position);

                if(_state != eState.Chase)
                    _state = eState.Chase;

                return;
            }

        if(_state == eState.Chase || _state == eState.Attack)
            _state = eState.Default;
    }

    /// <summary>
    /// Движение к цели
    /// </summary>
    /// <param name="targetPosition">Цель</param>
    /// <param name="extraSpeed">Прибавка к скорости</param>
    protected void MoveTo(Vector2 targetPosition, float extraSpeed = 0)
    {
        float totalSpeed = (Speed.Value + extraSpeed) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, totalSpeed);
        _animation.PlayRUNNING(_movementDirection);
    }

    /// <summary>
    /// Движение к цели
    /// </summary>
    /// <param name="targetPosition">Цель</param>
    /// <param name="finishFunction">Функция по окончании движения</param>
    /// <param name="extraSpeed">Прибавка к скорости</param>
    protected void MoveTo(Vector2 targetPosition, System.Action finishFunction, float extraSpeed = 0)
    {
        float totalSpeed = (Speed.Value + extraSpeed) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, totalSpeed);
        _animation.PlayRUNNING(_movementDirection, finishFunction);
    }

    /// <summary>
    /// Устанавливает направление
    /// </summary>
    /// <param name="direction">Направление</param>
    protected void SetDirection(Vector2 direction)
    {
        _movementDirection = Direction2D.GetDirectionTo(transform.position, direction);

        CheckDirection();
    }

    /// <summary>
    /// Проверяет направление
    /// </summary>
    protected void CheckDirection()
    {
        _directionManager.ChangeDirection(_movementDirection);
        _animation.PlayIDLE(_movementDirection);
    }

    #region Gizmos

    Vector2 _ap;
    float _ar;
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_ap, _ar);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(ChaseRadius));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(BtwTargetDistance));
    }

    #endregion
}
