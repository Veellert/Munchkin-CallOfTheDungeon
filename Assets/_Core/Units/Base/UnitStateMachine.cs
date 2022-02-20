/// <summary>
/// Родитель машины состояний юнита
/// </summary>
public abstract class UnitStateMachine
{
    //===>> Components & Fields <<===\\

    private IUnitState _currentState;

    private bool _inTransition;

    //===>> Public Methods <<===\\

    /// <summary>
    /// Выполняет действие текущего состояния
    /// </summary>
    public virtual void ExecuteCurrent()
    {
        if(!_inTransition)
            _currentState?.OnExecute();
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// Назначение текущего состояния юнита
    /// </summary>
    /// <param name="state">Состояние юнита</param>
    protected void TransitTo(IUnitState state)
    {
        if (state == null)
            return;
        else if (_currentState == state)
            return;

        _inTransition = true;
        _currentState?.OnExit();

        _currentState = state;

        _currentState?.OnEnter();
        _inTransition = false;
    }
}
