using System;

/// <summary>
/// Состояние игры
/// </summary>
public class GameState
{
    public enum eState
    {
        Play,
        Pause,
        Cheat,
    }

    /// <returns>
    /// Активное состояние
    /// </returns>
    /// <param name="stateAction">Действие которое происходит во время этого состояния</param>
    public static GameState PlayState(Action stateAction = null) => new GameState(eState.Play, stateAction);
    /// <returns>
    /// Состояние паузы
    /// </returns>
    /// <param name="stateAction">Действие которое происходит во время этого состояния</param>
    public static GameState PauseState(Action stateAction = null) => new GameState(eState.Pause, stateAction);
    /// <returns>
    /// Состояние Чит
    /// </returns>
    /// <param name="stateAction">Действие которое происходит во время этого состояния</param>
    public static GameState CheatState(Action stateAction = null) => new GameState(eState.Cheat, stateAction);

    public eState State { get; private set; }

    private Action _stateAction;

    private GameState(eState gameState, Action stateAction)
    {
        State = gameState;
        InitializeAction(stateAction);
    }

    /// <summary>
    /// Устанавливает действие для состояния
    /// </summary>
    /// <param name="stateAction">Действие которое происходит во время этого состояния</param>
    public void InitializeAction(Action stateAction) => _stateAction = stateAction;

    /// <summary>
    /// Выполняет действие состояния
    /// </summary>
    public void InvokeState() => _stateAction?.Invoke();
}