using Assets.DropSystem;
using UnityEngine;

public class PollymorphMonster : Monster
{
    private DropSystem _drop;
    private GameObject _target;

    public void InitializePollymorph(DropSystem drop, BaseUnit target)
    {
        _drop = drop;
        _target = target.gameObject;
    }

    protected override void InitializeStates()
    {
        base.InitializeStates();

        StateMachine.InitializeState(_dieState,
            onEnter: OnDieEnter);
    }

    private void OnDieEnter()
    {
        _drop.AddDropToInventory();
        Destroy(_target);
    }

    protected override void ChaseInput()
    {

    }
}
