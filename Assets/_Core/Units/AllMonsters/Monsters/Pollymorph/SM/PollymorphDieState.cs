using Assets.DropSystem;

/// <summary>
/// Состояние смерти монстра полиморфа
/// </summary>
public class PollymorphDieState : BaseMonsterDieState
{
    //===>> Components & Fields <<===\\

    protected DropSystem _drop;

    //===>> Constructor <<===\\

    public PollymorphDieState(PollymorphMonster monster, float destroyAfterDeathDelay, DropSystem drop) :
        base(monster, destroyAfterDeathDelay)
    {
        _drop = drop;
    }

    //===>> Interfaces Methods <<===\\

    public override void OnEnter()
    {
        _drop?.AddDropToInventory();
        base.OnEnter();
    }
}
