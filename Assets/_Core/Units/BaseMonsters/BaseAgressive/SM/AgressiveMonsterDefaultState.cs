using UnityEngine;

/// <summary>
/// Обычное состояние агрессивного монстра
/// </summary>
public class AgressiveMonsterDefaultState : BaseMonsterDefaultState
{
    //===>> Properties <<===\\

    protected NumericAttrib AttackCooldown => _monster.AttackCooldown;

    //===>> Components & Fields <<===\\

    protected new AgressiveMonster _monster;

    //===>> Constructor <<===\\

    public AgressiveMonsterDefaultState(MonsterStateMachine stateMachine, AgressiveMonster monster, float tileHalfDisableDistance) :
        base(stateMachine, monster, tileHalfDisableDistance)
    {
        _monster = monster;
    }

    //===>> Interfaces Methods <<===\\

    public override void OnExecute()
    {
        AttackCooldown.DecreaseOnDeltaTime();
        base.OnExecute();
    }
}
