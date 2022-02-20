using UnityEngine;

/// <summary>
/// Cостояние преследования агрессивного монстра
/// </summary>
public class AgressiveMonsterChaseState : BaseMonsterChaseState
{
    //===>> Properties <<===\\

    protected NumericAttrib AttackCooldown => _monster.AttackCooldown;
    protected bool CanAttack => _monster.CanAttack;

    //===>> Components & Fields <<===\\

    protected new AgressiveMonsterStateMachine _stateMachine;
    protected new AgressiveMonster _monster;

    //===>> Constructor <<===\\

    public AgressiveMonsterChaseState(AgressiveMonsterStateMachine stateMachine, AgressiveMonster monster) : base(stateMachine, monster)
    {
        _stateMachine = stateMachine;
        _monster = monster;
    }

    //===>> Interfaces Methods <<===\\

    public override void OnExecute()
    {
        AttackCooldown.DecreaseOnDeltaTime();

        base.OnExecute();

        if (Transform.DistanceTo(Player.Instance.transform) <= BtwTargetDistance.TileHalfed() && CanAttack)
            _stateMachine.TransitToAttack();
    }
}
