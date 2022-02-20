/// <summary>
/// Машина состояний игрока
/// </summary>
public class PlayerStateMachine : UnitStateMachine
{
    //===>> States <<===\\

    public IUnitState Default { get; }
    public IUnitState Dodge { get; }
    public IUnitState Attack { get; }
    public IUnitState Die { get; }

    //===>> Constructor <<===\\

    public PlayerStateMachine()
    {
        Default = new PlayerDefaultState();
        Dodge = new PlayerDodgeState(this);
        Attack = new PlayerAttackState(this);
        Die = new PlayerDieState();
    }

    //===>> Transit Methods <<===\\

    /// <summary>
    /// Переход в обычное состояние
    /// </summary>
    public void TransitToDefault() => TransitTo(Default);
    /// <summary>
    /// Переход в состояние рывка
    /// </summary>
    public void TransitToDodge() => TransitTo(Dodge);
    /// <summary>
    /// Переход в состояние атаки
    /// </summary>
    public void TransitToAttack() => TransitTo(Attack);
    /// <summary>
    /// Переход в состояние смерти
    /// </summary>
    public void TransitToDie() => TransitTo(Die);
}