/// <summary>
/// Машина состояний монстра передвигающегося по области
/// </summary>
public class IdleRangeMoveMonsterStateMachine : AgressiveMonsterStateMachine
{
    //===>> Properties <<===\\

    public new IdleRangeMoveMonster Monster { get; }

    //===>> Constructor <<===\\

    public IdleRangeMoveMonsterStateMachine(AgressiveMonsterStateMachine parentSM) : base(parentSM)
    {
        Monster = (IdleRangeMoveMonster)parentSM.Monster;

        Default = new IdleRangeMoveMonsterDefaultState(this, Monster, TileHalfDisableDistance);
    }
}