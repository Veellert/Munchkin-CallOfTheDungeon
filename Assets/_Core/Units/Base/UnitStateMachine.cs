/// <summary>
/// �������� ������ ��������� �����
/// </summary>
public abstract class UnitStateMachine
{
    //===>> Components & Fields <<===\\

    private IUnitState _currentState;

    private bool _inTransition;

    //===>> Public Methods <<===\\

    /// <summary>
    /// ��������� �������� �������� ���������
    /// </summary>
    public virtual void ExecuteCurrent()
    {
        if(!_inTransition)
            _currentState?.OnExecute();
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// ���������� �������� ��������� �����
    /// </summary>
    /// <param name="state">��������� �����</param>
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
