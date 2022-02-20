using System.Collections.Generic;

/// <summary>
/// Состояние юнита
/// </summary>
public class UnitState
{
    public delegate void OnState();

    /// <summary>
    /// При вхождении в состояние
    /// </summary>
    public OnState OnEnter;
    /// <summary>
    /// При выполнении состояния
    /// </summary>
    public OnState OnExecute;
    /// <summary>
    /// При выходе из в состояния
    /// </summary>
    public OnState OnExit;

    public UnitState(OnState onEnter, OnState onExecute, OnState onExit)
    {
        OnEnter = onEnter;
        OnExecute = onExecute;
        OnExit = onExit;
    }

    public static bool operator ==(UnitState a, UnitState b) => a?.GetHashCode() == b?.GetHashCode();
    public static bool operator !=(UnitState a, UnitState b) => a?.GetHashCode() != b?.GetHashCode();

    public override bool Equals(object obj)
    {
        return obj is UnitState state &&
               EqualityComparer<OnState>.Default.Equals(OnEnter, state.OnEnter) &&
               EqualityComparer<OnState>.Default.Equals(OnExecute, state.OnExecute) &&
               EqualityComparer<OnState>.Default.Equals(OnExit, state.OnExit);
    }
    public override int GetHashCode()
    {
        int hashCode = 1745298373;
        hashCode = hashCode * -1521134295 + EqualityComparer<OnState>.Default.GetHashCode(OnEnter);
        hashCode = hashCode * -1521134295 + EqualityComparer<OnState>.Default.GetHashCode(OnExecute);
        hashCode = hashCode * -1521134295 + EqualityComparer<OnState>.Default.GetHashCode(OnExit);
        return hashCode;
    }
}