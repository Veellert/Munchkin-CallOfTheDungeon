using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Класс отвечающий за логику состояний юнита
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
    /// Начало работы
    /// </summary>
    /// <param name="startState">Начальное состояние</param>
    public void Start(UnitState startState = null)
    {
        CurrentState = new UnitState("Current");

        if (startState == null)
            startState = _unitStateList[0];

        EnterTo(startState);
    }

    /// <summary>
    /// Добавлет новое состояние
    /// </summary>
    /// <param name="state">Новое состояние</param>
    public void Add(UnitState state)
    {
        if (_unitStateList.Exists(s => s == state))
            return;

        _unitStateList.Add(state);
    }

    /// <summary>
    /// Добавлет список новых состояний
    /// </summary>
    /// <param name="allStates">Список новых состояний</param>
    public void AddRange(IEnumerable<UnitState> allStates)
    {
        foreach (var state in allStates)
            Add(state);
    }

    /// <summary>
    /// Инициализирует состояние юнита
    /// </summary>
    /// <param name="state">Состояние юнита</param>
    /// <param name="onExecute">Действие при выполнении состояния</param>
    /// <param name="onEnter">Действие при вхождении в состояние</param>
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
    /// Является ли состояние текущим
    /// </returns>
    /// <param name="state">Состояние</param>
    public bool IsCurrent(UnitState state) => CurrentState == state;

    /// <summary>
    /// Назначение текущего состояния юнита
    /// </summary>
    /// <param name="state">Состояние юнита</param>
    public void EnterTo(UnitState state)
    {
        if (CurrentState != state)
            CurrentState.Enter(_unitStateList.FirstOrDefault(s => s == state));
    }

    /// <summary>
    /// Выполняет действие состояния
    /// </summary>
    /// <param name="condition">Условие</param>
    public void ExecuteCurrent(bool condition = true) => CurrentState?.Execute(condition);
}