/// <summary>
/// Машина состояний 1 босса 1 уровня
/// </summary>
public class SmilyFaceBossStateMachine : AgressiveMonsterStateMachine
{
    //===>> Properties <<===\\

    public new SmileyFaceBoss Monster { get; }

    //===>> States <<===\\

    public IUnitState SpecialAttack { get; protected set; }

    //===>> Constructor <<===\\

    public SmilyFaceBossStateMachine(AgressiveMonsterStateMachine parentSM) :
        base(parentSM)
    {
        Monster = (SmileyFaceBoss)parentSM.Monster;

        SpecialAttack = new SmileyFaceBossSpecialAttackState(Monster);
    }

    //===>> Transit Methods <<===\\

    /// <summary>
    /// Переход в состояние особой атаки
    /// </summary>
    public void TransitToSpecialAttack() => TransitTo(SpecialAttack);
}