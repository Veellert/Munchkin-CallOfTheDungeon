using System;

/// <summary>
/// Логика поведения босса в фазе
/// </summary>
public class BossPhaseScript
{
    //===>> Attributes & Properties <<===\\

    public string Label { get; private set; }
    public Action Script { get; private set; }

    //===>> Constructor <<===\\

    public BossPhaseScript(string label, Action script)
    {
        Label = label;
        Script = script;
    }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Выполняет логику вызванной фазы босса
    /// </summary>
    /// <returns>
    /// Вызванная фаза босса
    /// </returns>
    /// <param name="caller">Фаза босса</param>
    public BossPhase Invoke(BossPhase caller)
    {
        Invoke();

        return caller;
    }
    /// <summary>
    /// Выполняет логику текущей фазы босса
    /// </summary>
    /// <returns>
    /// Текущая фаза босса
    /// </returns>
    public BossPhaseScript Invoke()
    {
        Script?.Invoke();

        return this;
    }
}