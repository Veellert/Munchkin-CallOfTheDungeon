/// <summary>
/// Обычное состояние игрока по умолчанию
/// </summary>
public class IdleRangeMoveMonsterDefaultState : AgressiveMonsterDefaultState
{
    //===>> Components & Fields <<===\\

    protected new IdleRangeMoveMonster _monster;

    //===>> Constructor <<===\\

    public IdleRangeMoveMonsterDefaultState(MonsterStateMachine stateMachine, IdleRangeMoveMonster monster, float tileHalfDisableDistance) :
        base(stateMachine, monster, tileHalfDisableDistance)
    {
        _monster = monster;
    }

    //===>> Interfaces Methods <<===\\

    public override void OnExecute()
    {
        base.OnExecute();

        _monster.SetDirectionTo(_monster.GetMovePoint());
        _monster.MoveTo(_monster.GetMovePoint());
        _monster.StayOnPoint();
    }
}
