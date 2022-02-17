using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику монстра
/// </summary>
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Monster : BaseUnit, IDamager, IDamageable
{
    private const float DestroySelfDelay = 3;

    private static readonly List<Monster> _monsterList = new List<Monster>();

    public static NumericAttrib MonstersCount { get; set; }

    /// <summary>
    /// Удаляет монстра из списка
    /// </summary>
    /// <param name="monster">Монстр</param>
    public static void RemoveMonsterFromStack(Monster monster)
    {
        if (!monster)
            return;

        _monsterList.Remove(monster);
        MonstersCount--;
    }
    /// <summary>
    /// Добавляет монстра в список
    /// </summary>
    /// <param name="monster">Монстр</param>
    public static void AddMonsterToStack(Monster monster)
    {
        if (!monster)
            return;

        _monsterList.Add(monster);

        if (_monsterList.Count > MonstersCount.MaxValue)
            MonstersCount.SetMax(_monsterList.Count);
        MonstersCount++;
    }
    /// <returns>
    /// Список всех мностров на уровне
    /// </returns>
    public static List<Monster> GetMonsters() => _monsterList;
    /// <returns>
    /// Самый близкий монстр к точке
    /// </returns>
    /// <param name="position">Точка</param>
    /// <param name="ParamName">Радиус</param>
    public static Monster GetClosestMonster(Vector2 position, float range)
    {
        var monsters = _monsterList.FindAll(s => GetDistance(position, s) < range + new TileHalf(s.HitboxDistance));
        Monster closestMonster = monsters.FirstOrDefault();

        foreach (var monster in monsters)
            if (GetDistance(position, monster) < GetDistance(position, closestMonster))
                closestMonster = monster;

        return closestMonster;

        static float GetDistance(Vector2 position, Monster monster)
            => Vector2.Distance(position, monster.transform.position + (Vector3)monster.HitboxOffset);
    }
    /// <returns>
    /// Босс по его типу
    /// </returns>
    /// <param name="bossType">Тип босса (typeof)</param>
    public static Boss GetBoss(Type bossType)
    {
        return (Boss)_monsterList.Find(s => s.GetType() == bossType);
    }


    [Header("Damager Attribs")]
    [SerializeField] private NumericAttrib _damage = new NumericAttrib(15, 100);
    public NumericAttrib Damage { get => _damage; protected set => _damage = value; }
    [SerializeField] private NumericAttrib _attackRange = new TileHalf(0.7f).Value;
    public NumericAttrib AttackRange { get => _attackRange; protected set => _attackRange = value; }
    [SerializeField] private NumericAttrib _attackCooldown = 1;
    public NumericAttrib AttackCooldown { get => _attackCooldown; protected set => _attackCooldown = value; }

    [Header("Damageable Attribs")]
    [SerializeField] private NumericAttrib _hp = 100;
    public NumericAttrib HP { get => _hp; protected set => _hp = value; }

    [Header("Monster Attribs")]
    [SerializeField] private NumericAttrib _chaseRadius = new NumericAttrib(4, 10);
    public NumericAttrib ChaseRadius { get => _chaseRadius; protected set => _chaseRadius = value; }
    [SerializeField] private NumericAttrib _btwTargetDistance = 1.3f;
    public NumericAttrib BtwTargetDistance { get => _btwTargetDistance; protected set => _btwTargetDistance = value; }

    public bool IsDead => HP.IsValueEmpty();
    public bool CanAttack => AttackCooldown.IsValueEmpty();


    protected readonly UnitState _disabledState = new UnitState("Disabled");
    protected readonly UnitState _chaseState = new UnitState("Chase");
    protected readonly UnitState _attackState = new UnitState("Attack");
    protected readonly UnitState _duringAttackState = new UnitState("DuringAttack");
    protected readonly UnitState _dieState = new UnitState("Die");


    private void Awake()
    {
        if (MonstersCount == null)
            MonstersCount = _monsterList.Count;
    }

    protected override void Start()
    {
        base.Start();

        AddMonsterToStack(this);
    }

    protected virtual void FixedUpdate()
    {
        if (!Player.Instance)
            return;

        CheckAllCooldowns();

        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > new TileHalf(20))
            StateMachine.EnterTo(_disabledState);
        else if (!IsDead && (StateMachine.IsCurrent(_disabledState) || Player.Instance.IsDead))
            StateMachine.EnterTo(_defaultState);

        StateMachine.ExecuteCurrent();
    }

    protected void OnDestroy()
    {
        if (!IsDead)
            RemoveMonsterFromStack(this);
    }

    /// <summary>
    /// Переустанавливает неактивное состояние
    /// </summary>
    public void SetDisableState(bool isDisabled)
    {
        if (isDisabled)
            StateMachine.EnterTo(_disabledState);
        else
            StateMachine.EnterTo(_defaultState);
    }

    public virtual void Die() => StateMachine.EnterTo(_dieState);
    public virtual void GetDamage(float damageAmount)
    {
        HP -= damageAmount;
    }
    public virtual void Heal(float healAmount)
    {
        HP += healAmount;
    }

    public virtual void AttackInput()
    {
        _animation.IDLE(Vector2.zero);
        if (CanAttack)
            StateMachine.EnterTo(_attackState);
    }
    public virtual void AttackHandler()
    {
        var attackOffset = new TileHalf(BtwTargetDistance);
        var attackPosition = (Vector2)transform.position + Direction2D.GetPlayerDirectionFrom(transform.position) * attackOffset;
        if (Vector2.Distance(Player.Instance.transform.position, transform.position) < attackOffset)
            attackPosition = Player.Instance.transform.position;

        _tempAttackPosition = attackPosition;

        StateMachine.EnterTo(_duringAttackState);
    }
    protected override void ReleaseAttack(IDamageable target, float damage)
    {
        var playerDistance = Player.Instance.transform.position + (Vector3)Player.Instance.HitboxOffset;
        if (Vector2.Distance(_tempAttackPosition, playerDistance) < AttackRange + new TileHalf(Player.Instance.HitboxDistance))
            base.ReleaseAttack(target, damage);
        AttackCooldown.FillToMax();
    }

    /// <summary>
    /// Начать преследование
    /// </summary>
    protected virtual void ChaseInput()
    {
        StateMachine.EnterTo(TryChaseState());

        UnitState TryChaseState()
        {
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= new TileHalf(ChaseRadius) && !Player.Instance.IsDead && !Player.Instance.IsInvisibleForMonster)
                return _chaseState;
            return _defaultState;
        }
    }
    /// <summary>
    /// Обработка преследования
    /// </summary>
    protected virtual void ChaseHandler()
    {
        ChaseInput();

        SetDirectionTo(Player.Instance.transform.position);
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= new TileHalf(BtwTargetDistance))
            AttackInput();
        else
            MoveTo(Player.Instance.transform.position, 1);
    }

    /// <summary>
    /// Устанавливает направление до цели
    /// </summary>
    /// <param name="targetPosition">Координата цели</param>
    protected void SetDirectionTo(Vector2 targetPosition)
    {
        SetDirection(Direction2D.GetDirectionTo(transform.position, targetPosition));
        VisualizeDirection(_movementDirection.x);
    }
    /// <summary>
    /// Обнуляет направление
    /// </summary>
    protected void SetDirectionTo() => SetDirectionTo(transform.position);

    protected override void CheckAllCooldowns()
    {
        AttackCooldown -= Time.deltaTime;
    }
    protected override void GetRequiredComponents()
    {

    }
    protected override void SubscribeOnEvents()
    {
        HP.OnValueChanged += OnHPChanged;
    }
    protected override void InitializeStates()
    {
        StateMachine.AddRange(new[]
        {
            _disabledState,
            _chaseState,
            _attackState,
            _duringAttackState,
            _dieState,
        });

        StateMachine.InitializeState(_defaultState,
            onExecute: OnExecuteDefault);

        StateMachine.InitializeState(_chaseState,
            onExecute: OnExecuteChase,
            onEnter: OnEnterChase);

        StateMachine.InitializeState(_duringAttackState,
            onEnter: OnExecuteDuringAttack);

        StateMachine.InitializeState(_dieState,
            onEnter: OnExecuteDie);
    }

    /// <summary>
    /// Логика преследования
    /// </summary>
    private void OnExecuteChase()
    {
        ChaseHandler();
    }
    /// <summary>
    /// Обычная логика
    /// </summary>
    private void OnExecuteDefault()
    {
        ChaseInput();
    }
    /// <summary>
    /// При начале проведении атаки
    /// </summary>
    private void OnExecuteDuringAttack()
    {
        _animation.ATTACK(() =>
        {
            ReleaseAttack(Player.Instance, Damage);

            ChaseInput();
        });
    }
    /// <summary>
    /// При начале смерти
    /// </summary>
    private void OnExecuteDie()
    {
        _animation.DIE(() =>
        {
            RemoveMonsterFromStack(this);
            Invoke(nameof(DestroySelf), DestroySelfDelay);
        });
        GetComponent<Collider2D>().isTrigger = true;
    }

    /// <summary>
    /// При начале преследования
    /// </summary>
    private void OnEnterChase()
    {
        SetDirectionTo(Player.Instance.transform.position);
    }

    /// <summary>
    /// Событие при изменении здоровья
    /// </summary>
    /// <param name="obj">Отслеживаемый атрибут</param>
    private void OnHPChanged(NumericAttrib obj)
    {
        if (IsDead && !StateMachine.IsCurrent(_dieState))
            Die();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(ChaseRadius));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(BtwTargetDistance));
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_tempAttackPosition, AttackRange);
    }
}
