using Assets.DropSystem;

/// <summary>
/// Компонент отвечающий за логику монстра полиморфа
/// </summary>
public class PollymorphMonster : BaseMonster
{
    //===>> Components & Fields <<===\\

    private DropSystem _drop;
    private BaseUnit _target;

    //===>> Important Methods <<===\\

    protected override void InitializeStateMachine()
    {
        base.InitializeStateMachine();

        _stateMachine = new PollymorphMonsterStateMachine((MonsterStateMachine)_stateMachine, _drop);
    }

    //===>> Public Methods <<===\\

    public void InitializePollymorph(DropSystem drop, BaseUnit target)
    {
        _drop = drop;
        _target = target;

        _target.DestroySelf();
    }
}
