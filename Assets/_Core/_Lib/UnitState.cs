using System;
using System.Collections.Generic;

/// <summary>
/// Состояние юнита
/// </summary>
public class UnitState
{
    public string Tag { get; private set; }

    /// <summary>
    /// При изменении состояния
    /// </summary>
    public event Action OnEnter;
    /// <summary>
    /// При выполнении состояния
    /// </summary>
    public event Action OnExecute;

    public UnitState(string tag) => Tag = tag;

    /// <summary>
    /// Изменяет текущий экземпляр состояния на новое состояние
    /// </summary>
    /// <param name="state">Новое состояние</param>
    public void Enter(UnitState state)
    {
        Tag = state.Tag;
        OnEnter = state.OnEnter;
        OnExecute = state.OnExecute;

        OnEnter?.Invoke();
    }

    /// <summary>
    /// Выполняет событие состояния
    /// </summary>
    /// <param name="condition">Условие</param>
    public void Execute(bool condition = true)
    {
        if (condition)
            OnExecute?.Invoke();
    }

    public override bool Equals(object obj)
    {
        return obj is UnitState state &&
               Tag == state.Tag;
    }
    public override int GetHashCode()
    {
        return 1005349675 + EqualityComparer<string>.Default.GetHashCode(Tag);
    }
    public static bool operator ==(UnitState a, UnitState b) => a?.Tag == b?.Tag;
    public static bool operator !=(UnitState a, UnitState b) => a?.Tag != b?.Tag;
}