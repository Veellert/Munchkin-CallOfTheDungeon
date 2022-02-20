
/// <summary>
/// Состояние особой атаки 1 босса 1 уровня
/// </summary>
public class SmileyFaceBossSpecialAttackState : IUnitState
{
    //===>> Properties <<===\\

    protected NumericAttrib HP => _monster.HP;
    protected NumericAttrib MinionCount => _monster.MinionCount;
    protected NumericAttrib SpawnMinionDelay => _monster.SpawnMinionDelay;

    //===>> Components & Fields <<===\\

    protected SmileyFaceBoss _monster;

    //===>> Constructor <<===\\

    public SmileyFaceBossSpecialAttackState(SmileyFaceBoss monster)
    {
        _monster = monster;
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        _monster.SetDirectionTo();

        HP.OnValueChanged -= _monster.OnHPHalf;
        HP.OnValueChanged += _monster.OnHpQuarter;
    }

    public virtual void OnExecute()
    {
        if (!MinionCount.IsValueFull())
            _monster.LoopProtectedInvoke(nameof(_monster.SpawnMinion), SpawnMinionDelay);
    }

    public virtual void OnExit()
    {
        HP.OnValueChanged -= _monster.OnHpQuarter;
        HP.OnValueChanged += _monster.OnHPHalf;
    }
}
