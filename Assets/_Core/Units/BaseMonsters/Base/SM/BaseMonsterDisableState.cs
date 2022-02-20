using UnityEngine;

/// <summary>
/// Неактивное состояние монстра по умолчанию
/// </summary>
public class BaseMonsterDisableState : IUnitState
{
    //===>> Properties <<===\\

    protected Transform Transform => _monster.transform;

    //===>> Components & Fields <<===\\

    protected MonsterStateMachine _stateMachine;

    protected BaseMonster _monster;
    protected AnimationCaller _animation;
    protected float _tileHalfDisableDistance;

    //===>> Constructor <<===\\

    public BaseMonsterDisableState(MonsterStateMachine stateMachine, BaseMonster monster, float tileHalfDisableDistance)
    {
        _stateMachine = stateMachine;
        _monster = monster;
        _tileHalfDisableDistance = tileHalfDisableDistance.TileHalfed();
        _animation = _monster.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        _monster.SetDirectionTo();
    }

    public virtual void OnExecute()
    {
        _animation.Disabled();

        if (Transform.DistanceTo(Player.Instance.transform) < _tileHalfDisableDistance)
            _stateMachine.TransitToDefault();
    }

    public virtual void OnExit()
    {
        _animation.IDLE(_monster.GetMovementDirection());
    }
}
