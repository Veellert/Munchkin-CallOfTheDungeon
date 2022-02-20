/// <summary>
/// Обычное состояние монстра полиморфа
/// </summary>
public class PollymorphDefaultState : IUnitState
{
    //===>> Components & Fields <<===\\

    protected PollymorphMonster _monster;

    //===>> Constructor <<===\\

    public PollymorphDefaultState(PollymorphMonster monster)
    {
        _monster = monster;
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {

    }

    public virtual void OnExecute()
    {
        _monster.SetDirectionToPlayer();
    }

    public virtual void OnExit()
    {

    }
}
