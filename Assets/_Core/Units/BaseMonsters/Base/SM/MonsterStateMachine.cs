/// <summary>
/// Машина состояний монстра
/// </summary>
public class MonsterStateMachine : UnitStateMachine
{
    //===>> Properties <<===\\

    public BaseMonster Monster { get; }
    public float TileHalfDisableDistance { get; }
    public float DestroyAfterDeathDelay { get; }

    //===>> States <<===\\

    public IUnitState Default { get; protected set; }
    public IUnitState Chase { get; protected set; }
    public IUnitState Die { get; protected set; }
    public IUnitState Disable { get; protected set; }

    //===>> Constructor <<===\\

    public MonsterStateMachine(BaseMonster monster, float tileHalfDisableDistance, float destroyAfterDeathDelay)
    {
        Monster = monster;
        TileHalfDisableDistance = tileHalfDisableDistance;
        DestroyAfterDeathDelay = destroyAfterDeathDelay;

        Default = new BaseMonsterDefaultState(this, Monster, TileHalfDisableDistance);
        Chase = new BaseMonsterChaseState(this, Monster);
        Die = new BaseMonsterDieState(Monster, DestroyAfterDeathDelay);
        Disable = new BaseMonsterDisableState(this, Monster, TileHalfDisableDistance);

        TransitToDisabled();
    }

    //===>> Important Methods <<===\\

    public override void ExecuteCurrent()
    {
        if (!Player.Instance)
            return;

        base.ExecuteCurrent();
    }

    //===>> Transit Methods <<===\\

    /// <summary>
    /// Переход в обычное состояние
    /// </summary>
    public void TransitToDefault() => TransitTo(Default);
    /// <summary>
    /// Переход в состояние преследования
    /// </summary>
    public void TransitToChase() => TransitTo(Chase);
    /// <summary>
    /// Переход в состояние смерти
    /// </summary>
    public void TransitToDie() => TransitTo(Die);
    /// <summary>
    /// Переход в неактивное состояние
    /// </summary>
    public void TransitToDisabled() => TransitTo(Disable);
}
