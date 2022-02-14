using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ����� ���������� �� ������ ��������� �����
/// </summary>
public class UnitStateMachine
{
    public UnitState CurrentState { get; private set; }

    private List<UnitState> _unitStateList = new List<UnitState>();

    public UnitStateMachine(IEnumerable<UnitState> allStates)
    {
        AddRange(allStates);
    }
    
    public UnitStateMachine(UnitState defaultState)
    {
        Add(defaultState);
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    /// <param name="startState">��������� ���������</param>
    public void Start(UnitState startState = null)
    {
        CurrentState = new UnitState("Current");

        if (startState == null)
            startState = _unitStateList[0];

        EnterTo(startState);
    }

    /// <summary>
    /// �������� ����� ���������
    /// </summary>
    /// <param name="state">����� ���������</param>
    public void Add(UnitState state)
    {
        if (_unitStateList.Exists(s => s == state))
            return;

        _unitStateList.Add(state);
    }

    /// <summary>
    /// �������� ������ ����� ���������
    /// </summary>
    /// <param name="allStates">������ ����� ���������</param>
    public void AddRange(IEnumerable<UnitState> allStates)
    {
        foreach (var state in allStates)
            Add(state);
    }

    /// <summary>
    /// �������������� ��������� �����
    /// </summary>
    /// <param name="state">��������� �����</param>
    /// <param name="onExecute">�������� ��� ���������� ���������</param>
    /// <param name="onEnter">�������� ��� ��������� � ���������</param>
    public void InitializeState(UnitState state, Action onExecute = null, Action onEnter = null)
    {
        var initState = _unitStateList.FirstOrDefault(s => s == state);

        if (initState == null)
            return;

        if(onExecute != null)
            initState.OnExecute += onExecute;
        if(onEnter != null)
            initState.OnEnter += onEnter;

        if (CurrentState == initState)
            CurrentState = initState;
    }

    /// <returns>
    /// �������� �� ��������� �������
    /// </returns>
    /// <param name="state">���������</param>
    public bool IsCurrent(UnitState state) => CurrentState == state;

    /// <summary>
    /// ���������� �������� ��������� �����
    /// </summary>
    /// <param name="state">��������� �����</param>
    public void EnterTo(UnitState state)
    {
        if (CurrentState != state)
            CurrentState.Enter(_unitStateList.FirstOrDefault(s => s == state));
    }

    /// <summary>
    /// ��������� �������� ���������
    /// </summary>
    /// <param name="condition">�������</param>
    public void ExecuteCurrent(bool condition = true) => CurrentState?.Execute(condition);
}