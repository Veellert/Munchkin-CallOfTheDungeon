using Assets.DropSystem;

/// <summary>
/// Машина состояний монстра полиморфа
/// </summary>
public class PollymorphMonsterStateMachine : MonsterStateMachine
{
    //===>> Properties <<===\\

    public new PollymorphMonster Monster { get; }

    //===>> Constructor <<===\\

    public PollymorphMonsterStateMachine(MonsterStateMachine parentSM, DropSystem drop) :
        base(parentSM.Monster, parentSM.TileHalfDisableDistance, parentSM.DestroyAfterDeathDelay)
    {
        Monster = (PollymorphMonster)parentSM.Monster;

        Default = new PollymorphDefaultState(Monster);
        Die = new PollymorphDieState(Monster, DestroyAfterDeathDelay, drop);
    }
}