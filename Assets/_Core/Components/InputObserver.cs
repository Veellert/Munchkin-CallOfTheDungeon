using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент который отслеживает пользовательский ввод
/// </summary>
public class InputObserver : MonoBehaviour
{
    public static InputObserver Instance { get; set; }

    /// <returns>
    /// Позиция курсора
    /// </returns>
    public static Vector3 GetMousePosition()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    [Header("Keyboard Buttons")]
    public InputButton _pauseMenu = new InputButton(KeyCode.Escape);
    public InputButton _minimap = new InputButton(KeyCode.Tab);
    public InputButton _minimapIncrease = new InputButton(KeyCode.Minus, true);
    public InputButton _minimapDecrease = new InputButton(KeyCode.Equals, true);
    public InputButton _cheatPanel = new InputButton(KeyCode.L);
    public InputButton _dodge = new InputButton(KeyCode.Space);

    /// <summary>
    /// Событие по клику левой кнопки мыши
    /// </summary>
    public event Action OnLeftMouseButton;

    /// <summary>
    /// Событие на передвижение влево и вправо
    /// </summary>
    public event Action<float> OnHorizontalAxisInput;
    /// <summary>
    /// Событие на передвижение вверх и вниз
    /// </summary>
    public event Action<float> OnVerticalAxisInput;

    private float _horizontalAxisInput;
    private float _verticalAxisInput;

    private List<GameState> _gameStateList = new List<GameState>();
    private GameState _currentGameState;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _gameStateList = new List<GameState>
        {
            GameState.PlayState(() =>
            {
                InputAxis(Input.GetAxisRaw("Horizontal"), ref _horizontalAxisInput, OnHorizontalAxisInput);
                InputAxis(Input.GetAxisRaw("Vertical"), ref _verticalAxisInput, OnVerticalAxisInput);

                _pauseMenu.UseButton(() =>
                {
                    ResetAxis();
                    SetGameState(GameState.PauseState());
                });
                _cheatPanel.UseButton(() =>
                {
                    ResetAxis();
                    SetGameState(GameState.CheatState());
                });
                _minimap.UseButton();
                _dodge.UseButton();

                _minimapIncrease.UseButton();
                _minimapDecrease.UseButton();

                InputMouse(0, OnLeftMouseButton);
            }),
            GameState.PauseState(() =>
            {
                _pauseMenu.UseButton(() => SetGameState(GameState.PlayState()));
            }),
            GameState.CheatState(() =>
            {
                _cheatPanel.UseButton(() => SetGameState(GameState.PlayState()));
            })
        };

        _currentGameState = _gameStateList[0];
    }

    private void Update()
    {
        _currentGameState?.InvokeState();
    }

    /// <returns>
    /// Направление игрока
    /// </returns>
    public Vector2 GetInputDirection() => new Vector2(_horizontalAxisInput, _verticalAxisInput).normalized;

    /// <summary>
    /// Устанавливает состояние игры
    /// </summary>
    /// <param name="state">Состояние игры</param>
    public void SetGameState(GameState state)
    {
        if (_currentGameState.State != state.State)
            _currentGameState = _gameStateList.Find(s => s.State == state.State);
    }

    /// <summary>
    /// Нажатие на кнопку мыши
    /// </summary>
    /// <param name="button">Номер кнопки</param>
    /// <param name="action">Действие по нажатию</param>
    private void InputMouse(int button, Action action)
    {
        if (Input.GetMouseButton(button))
            action?.Invoke();
    }

    /// <summary>
    /// Ввод оси передвижения
    /// </summary>
    /// <param name="axisInput">Введенное значение оси</param>
    /// <param name="axis">Текущее значение оси</param>
    /// <param name="action">Действие по изменению значения</param>
    private void InputAxis(float axisInput, ref float axis, Action<float> action)
    {
        if (axis == axisInput)
            return;
        
        axis = axisInput;
        action?.Invoke(axis);
    }
    /// <summary>
    /// Сбрасывает оси передвижения
    /// </summary>
    private void ResetAxis()
    {
        InputAxis(0, ref _horizontalAxisInput, OnHorizontalAxisInput);
        InputAxis(0, ref _verticalAxisInput, OnVerticalAxisInput);
    }
}
