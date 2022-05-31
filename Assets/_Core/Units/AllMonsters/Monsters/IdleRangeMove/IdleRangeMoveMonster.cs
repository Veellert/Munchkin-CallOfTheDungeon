using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику подвижного монстра
/// </summary>
public class IdleRangeMoveMonster : AgressiveMonster, IIdleMovable
{
    //===>> Attributes & Properties <<===\\

    public NumericAttrib IdleMovementRadius { get; protected set ; }
    public NumericAttrib StayPointTime { get; protected set ; }
    public NumericAttrib RemovePointTime { get; protected set ; }

    protected new IdleMovableMonsterPreferences MonsterPreferences => (IdleMovableMonsterPreferences)_preferences;

    //===>> Components & Fields <<===\\

    private Vector2 _movePoint;
    private Vector2 _startPoint;

    //===>> Important Methods <<===\\

    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        IdleMovementRadius = MonsterPreferences.IdleMovementRadius;
        StayPointTime = MonsterPreferences.StayPointTime;
        RemovePointTime = MonsterPreferences.RemovePointTime;

        _startPoint = _movePoint = transform.position;
        InitPoint();
    }
    protected override void InitializeStateMachine()
    {
        base.InitializeStateMachine();

        _stateMachine = new IdleRangeMoveMonsterStateMachine((AgressiveMonsterStateMachine)_stateMachine);
    }

    //===>> Interfaces Methods <<===\\

    public virtual void StayOnPoint()
    {
        var (methodName, delay) = GetInvokeInfo(Vector2.Distance(transform.position, _movePoint) < 0.2f);
        this.LoopProtectedInvoke(methodName, delay);
        
        (string methodName, float delay) GetInvokeInfo(bool condition)
        {
            if (condition)
                return (nameof(InitPoint), StayPointTime);
            return (nameof(RemovePoint), RemovePointTime);
        }
    }
    public virtual void InitPoint()
    {
        _movePoint = GetRandomIdleRadiusPosition();
    }
    public virtual void RemovePoint()
    {
        this.LoopProtectedInvoke(nameof(InitPoint), InitPoint);
    }

    //===>> Public Methods <<===\\

    /// <returns>
    /// Точка передвижения
    /// </returns>
    public Vector2 GetMovePoint() => _movePoint;

    //===>> Private & Protected Methods <<===\\

    /// <returns>
    /// Рандомная координата для точки
    /// </returns>
    protected Vector2 GetRandomIdleRadiusPosition()
    {
        return _startPoint + Random.insideUnitCircle * IdleMovementRadius.TileHalfed();
    }

    //===>> Gizmos <<===\\

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.green;
        #region IdleMovement

        if(IdleMovementRadius != null)
            Gizmos.DrawWireSphere(_startPoint, IdleMovementRadius.TileHalfed());
        else
            Gizmos.DrawWireSphere(transform.position, MonsterPreferences.IdleMovementRadius.TileHalfed());

        #endregion
    }
}
