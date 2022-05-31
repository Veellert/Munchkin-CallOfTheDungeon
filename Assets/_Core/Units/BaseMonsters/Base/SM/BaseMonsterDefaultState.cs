using UnityEngine;

/// <summary>
/// Обычное состояние монстра по умолчанию
/// </summary>
public class BaseMonsterDefaultState : IUnitState
{
    //===>> Properties <<===\\

    protected Transform Transform => _monster.transform;
    protected bool CanChase => _monster.CanChase;

    //===>> Components & Fields <<===\\

    protected MonsterStateMachine _stateMachine;

    protected BaseMonster _monster;
    protected float _tileHalfDisableDistance;
    protected AnimationCaller _animation;

    //===>> Constructor <<===\\

    public BaseMonsterDefaultState(MonsterStateMachine stateMachine, BaseMonster monster, float tileHalfDisableDistance)
    {
        _stateMachine = stateMachine;
        _monster = monster;
        _tileHalfDisableDistance = tileHalfDisableDistance.TileHalfed();
        _animation = _monster.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        _animation.IDLE(_monster.GetMovementDirection());
    }

    public virtual void OnExecute()
    {
        if (Transform.DistanceTo(Player.Instance.transform) >= _tileHalfDisableDistance)
        {
            _stateMachine.TransitToDisabled();
            return;
        }
        
        if (CanChase)
            _stateMachine.TransitToChase();
    }

    public virtual void OnExit()
    {

    }
}
