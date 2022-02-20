using UnityEngine;

/// <summary>
/// Cостояние преследования монстра по умолчанию
/// </summary>
public class BaseMonsterChaseState : IUnitState
{
    //===>> Properties <<===\\

    protected Transform Transform => _monster.transform;
    protected NumericAttrib BtwTargetDistance => _monster.BtwTargetDistance;
    protected bool CanChase => _monster.CanChase;

    //===>> Components & Fields <<===\\

    protected MonsterStateMachine _stateMachine;

    protected BaseMonster _monster;
    protected AnimationCaller _animation;

    //===>> Constructor <<===\\

    public BaseMonsterChaseState(MonsterStateMachine stateMachine, BaseMonster monster)
    {
        _stateMachine = stateMachine;
        _monster = monster;
        _animation = _monster.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        _monster.SetDirectionToPlayer();
    }

    public virtual void OnExecute()
    {
        if(!CanChase)
            _stateMachine.TransitToDefault();

        _monster.SetDirectionToPlayer();

        if (Transform.DistanceTo(Player.Instance.transform) <= BtwTargetDistance.TileHalfed())
            _animation.IDLE(Vector2.zero);
        else
            _monster.MoveTo(Player.Instance.transform.position, 1);
    }

    public virtual void OnExit()
    {

    }
}
