using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику монстра
/// </summary>
public abstract partial class BaseMonster : MobileUnit, IDamageable
{
    //===>> Constants <<===\\

    protected const float DestroyAfterDeathDelay = 3;
    protected const float TileHalfDisableDistance = 20;

    //===>> Inspector <<===\\

    [Header("Monster Attribs")]
    [SerializeField] protected NumericAttrib _btwTargetDistance = 1.3f;

    //===>> Attributes & Properties <<===\\

    public NumericAttrib BtwTargetDistance => _btwTargetDistance;

    public NumericAttrib ChaseRadius { get ; protected set; }
    public bool CanChase => 
        transform.DistanceToPlayer() <= ChaseRadius.TileHalfed() &&
        !Player.Instance.IsDead && !Player.Instance.IsInvisibleForMonster;

    protected MonsterPreferences MonsterPreferences => (MonsterPreferences)_preferences;

    //===>> Unity <<===\\

    protected void OnDestroy()
    {
        if (!IsDead)
            RemoveMonsterFromStack(this);
    }

    //===>> Important Methods <<===\\

    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        ChaseRadius = MonsterPreferences.ChaseRadius;

        HP.OnValueChanged += OnHPChanged;
        AddMonsterToStack(this);
    }
    protected override void InitializeStateMachine()
    {
        _stateMachine = new MonsterStateMachine(this, TileHalfDisableDistance, DestroyAfterDeathDelay);

        ((MonsterStateMachine)_stateMachine).TransitToDefault();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void Die() => ((MonsterStateMachine)_stateMachine).TransitToDie();

    public virtual void Heal(float healAmount) => HP += healAmount;

    public virtual void ReceiveDamage(float damageAmount) => HP -= damageAmount;

    //===>> Public Methods <<===\\

    /// <summary>
    /// Переустанавливает неактивное состояние
    /// </summary>
    /// <param name="isDisabled">Переключатель</param>
    public void SetDisableState(bool isDisabled)
    {
        if (isDisabled)
            ((MonsterStateMachine)_stateMachine).TransitToDisabled();
        else
            ((MonsterStateMachine)_stateMachine).TransitToDefault();
    }

    /// <summary>
    /// Установить направление на игрока
    /// </summary>
    public void SetDirectionToPlayer() => SetDirectionTo(Player.Instance.transform.position);
    /// <summary>
    /// Устанавливает направление до цели
    /// </summary>
    /// <param name="targetPosition">Координата цели</param>
    public void SetDirectionTo(Vector2 targetPosition)
    {
        SetDirection(Direction2D.GetDirectionTo(transform.position, targetPosition));
    }
    /// <summary>
    /// Обнуляет направление
    /// </summary>
    public void SetDirectionTo() => SetDirectionTo(transform.position);

    /// <summary>
    /// Движение к цели
    /// </summary>
    /// <param name="targetPosition">Цель</param>
    /// <param name="extraSpeed">Прибавка к скорости</param>
    public void MoveTo(Vector2 targetPosition, float extraSpeed = 0)
    {
        float totalSpeed = (Speed.Value + extraSpeed) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, totalSpeed);
        _animation.RUNNING(_movementDirection);
    }
    /// <summary>
    /// Движение к цели
    /// </summary>
    /// <param name="targetPosition">Цель</param>
    /// <param name="finishFunction">Функция по окончании движения</param>
    /// <param name="extraSpeed">Прибавка к скорости</param>
    public void MoveTo(Vector2 targetPosition, Action finishFunction, float extraSpeed = 0)
    {
        float totalSpeed = (Speed.Value + extraSpeed) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, totalSpeed);
        _animation.RUNNING(_movementDirection, finishFunction);
    }

    //===>> On Events <<===\\

    /// <summary>
    /// Событие при изменении здоровья
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void OnHPChanged(NumericAttrib hp)
    {
        if (IsDead)
            Die();
    }

    //===>> Gizmos <<===\\

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        #region Chase

        if(ChaseRadius != null)
            Gizmos.DrawWireSphere(transform.position, ChaseRadius.TileHalfed());
        else
            Gizmos.DrawWireSphere(transform.position, MonsterPreferences.ChaseRadius.TileHalfed());

        #endregion

        Gizmos.color = Color.yellow;
        #region BtwTarget

        if(BtwTargetDistance != null)
            Gizmos.DrawWireSphere(transform.position, BtwTargetDistance.TileHalfed());

        #endregion
    }
}

public abstract partial class BaseMonster
{
    private static readonly List<BaseMonster> _monsterList = new List<BaseMonster>();

    public static NumericAttrib MonstersCount { get; set; } = new NumericAttrib();

    public static List<BaseMonster> GetMonsters() => _monsterList;

    /// <summary>
    /// Удаляет монстра из списка
    /// </summary>
    /// <param name="monster">Монстр</param>
    public static void RemoveMonsterFromStack(BaseMonster monster)
    {
        if (monster == null)
            return;

        _monsterList.Remove(monster);
        MonstersCount--;
    }
    /// <summary>
    /// Добавляет монстра в список
    /// </summary>
    /// <param name="monster">Монстр</param>
    public static void AddMonsterToStack(BaseMonster monster)
    {
        if (monster == null)
            return;

        _monsterList.Add(monster);

        if (_monsterList.Count > MonstersCount.MaxValue)
            MonstersCount.SetMax(_monsterList.Count);
        MonstersCount++;
    }

    /// <returns>
    /// Самый близкий монстр к точке
    /// </returns>
    /// <param name="position">Точка</param>
    /// <param name="ParamName">Радиус</param>
    public static BaseMonster GetClosest(Vector2 position, float range)
    {
        var monsters = _monsterList.FindAll(monster => GetDistance(position, monster) < range + monster.HitboxRange.TileHalfed());
        var closestMonster = monsters.FirstOrDefault();

        foreach (var monster in monsters)
            if (GetDistance(position, monster) < GetDistance(position, closestMonster))
                closestMonster = monster;

        return closestMonster;

        static float GetDistance(Vector2 position, BaseMonster monster)
            => Vector2.Distance(position, monster.transform.position + (Vector3)monster.HitboxOffset);
    }

    protected override void Awake()
    {
        base.Awake();

        if (MonstersCount == null)
            MonstersCount = _monsterList.Count;
    }
}
