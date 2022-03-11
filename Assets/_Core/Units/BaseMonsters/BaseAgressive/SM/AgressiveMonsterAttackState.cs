using UnityEngine;

/// <summary>
/// Cостояние атаки агрессивного монстра
/// </summary>
public class AgressiveMonsterAttackState : IUnitState
{
    //===>> Properties <<===\\

    protected Transform Transform => _monster.transform;
    protected NumericAttrib AttackDistance => _monster.BtwTargetDistance.TileHalfed();
    protected NumericAttrib AttackCooldown => _monster.AttackCooldown;
    protected NumericAttrib AttackRange => _monster.AttackRange;
    protected NumericAttrib Damage => _monster.Damage;

    //===>> Components & Fields <<===\\

    protected AgressiveMonsterStateMachine _stateMachine;
    protected AgressiveMonster _monster;
    protected AnimationCaller _animation;

    //===>> Constructor <<===\\

    public AgressiveMonsterAttackState(AgressiveMonsterStateMachine stateMachine, AgressiveMonster monster)
    {
        _stateMachine = stateMachine;
        _monster = monster;
        _animation = _monster.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        _monster.SetTempAttackPosition((Vector2)Transform.position + Direction2D.GetPlayerDirectionFrom(Transform.position) * AttackDistance);
        if (Transform.DistanceToPlayer() < AttackDistance)
            _monster.SetTempAttackPosition(Player.Instance.transform.position);

        _animation.ATTACK(() =>
        {
            _stateMachine.TransitToDefault();
        });
    }

    public virtual void OnExecute()
    {
        
    }

    public virtual void OnExit()
    {
        AttackCooldown.FillToMax();

        var playerHitboxPosition = Player.Instance.transform.position + (Vector3)Player.Instance.HitboxOffset;
        if (Vector2.Distance(_monster.GetTempAttackPosition(), playerHitboxPosition) < AttackRange + Player.Instance.HitboxRange.TileHalfed())
            _monster.AttackTo(Player.Instance, Damage);
    }
}
