using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputButton
{
    [SerializeField] private KeyCode _buttonCode;
    public KeyCode Button
    {
        get { return _buttonCode; }
        set { _buttonCode = value; }
    }

    [HideInInspector] public event EventHandler OnButtonEvent;

    public InputButton(KeyCode buttonCode)
    {
        Button = buttonCode;
    }

    public void Invoke() => OnButtonEvent?.Invoke(null, EventArgs.Empty);
}

public enum eGameState
{
    Play,
    Pause,
    Cheat,
}

public class InputObserver : MonoBehaviour
{
    public static InputObserver Instance { get; set; }

    public InputButton pauseMenu = new InputButton(KeyCode.Escape);
    public InputButton minimap = new InputButton(KeyCode.Tab);
    public InputButton minimapIncrease = new InputButton(KeyCode.Minus);
    public InputButton minimapDecrease = new InputButton(KeyCode.Equals);

    public InputButton cheatPanel = new InputButton(KeyCode.L);

    private eGameState _gameState = eGameState.Play;

    private void Start()
    {
        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        switch (_gameState)
        {
            case eGameState.Play:
                PressButton(pauseMenu, ()
                    => { _gameState = eGameState.Pause; });
                PressButton(minimap);
                HoldButton(minimapIncrease);
                HoldButton(minimapDecrease);
                PressButton(cheatPanel, ()
                    => { _gameState = eGameState.Cheat; });
                break;
            case eGameState.Pause:
                PressButton(pauseMenu, ()
                    => { _gameState = eGameState.Play; });
                break;
            case eGameState.Cheat:
                PressButton(cheatPanel, ()
                    => { _gameState = eGameState.Play; });
                break;
        }
    }

    public void SetGameState(eGameState state)
    {
        if(_gameState != state)
            _gameState = state;
    }

    private void PressButton(InputButton button, Action action = null)
    {
        if (Input.GetKeyDown(button.Button))
            UseButton(button, action);
    }
    private void HoldButton(InputButton button, Action action = null)
    {
        if (Input.GetKey(button.Button))
            UseButton(button, action);
    }

    private void UseButton(InputButton button, Action action = null)
    {
        action?.Invoke();
        button.Invoke();
    }
}
