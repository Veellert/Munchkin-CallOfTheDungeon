/// <summary>
/// Машина состояний агрессивного монстра
/// </summary>
public class AgressiveMonsterStateMachine : MonsterStateMachine
{
    //===>> Properties <<===\\

    public new AgressiveMonster Monster { get; }

    //===>> States <<===\\

    public IUnitState Attack { get; protected set; }

    //===>> Constructor <<===\\

    public AgressiveMonsterStateMachine(MonsterStateMachine parentSM) :
        base(parentSM.Monster, parentSM.TileHalfDisableDistance, parentSM.DestroyAfterDeathDelay)
    {
        Monster = (AgressiveMonster)parentSM.Monster;

        Default = new AgressiveMonsterDefaultState(this, Monster, TileHalfDisableDistance);
        Chase = new AgressiveMonsterChaseState(this, Monster);
        Attack = new AgressiveMonsterAttackState(this, Monster);
    }

    //===>> Transit Methods <<===\\

    /// <summary>
    /// Переход в состояние атаки
    /// </summary>
    public void TransitToAttack() => TransitTo(Attack);
}
