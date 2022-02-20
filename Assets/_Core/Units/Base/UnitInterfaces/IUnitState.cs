/// <summary>
/// Состояние юнита
/// </summary>
public interface IUnitState
{
    /// <summary>
    /// На входе в состояние
    /// </summary>
    void OnEnter();
    /// <summary>
    /// При пребывании в состоянии
    /// </summary>
    void OnExecute();
    /// <summary>
    /// На выходе из состояния
    /// </summary>
    void OnExit();
}
