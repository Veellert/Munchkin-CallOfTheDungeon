using System;
using UnityEngine;

/// <summary>
/// Кнопка на клавиатуре
/// </summary>
[Serializable]
public class InputButton
{
    [Header("Need Hold Button")]
    [SerializeField] private bool _isButtonHold;
    public bool IsButtonHold { get => _isButtonHold; private set => _isButtonHold = value; }

    [Header("Button Code")]
    [SerializeField] private KeyCode _button;
    public KeyCode Button { get => _button; private set => _button = value; }

    /// <summary>
    /// Событие по нажатию на кнопку
    /// </summary>
    public event Action<InputButton> OnButtonUse;

    public InputButton(KeyCode button, bool isButtonHold = false)
    {
        Button = button;
        IsButtonHold = isButtonHold;
    }

    /// <summary>
    /// Нажать на кнопку
    /// </summary>
    /// <param name="subAction">Дополнительное действие</param>
    public void UseButton(Action subAction = null) => Invoke(subAction, IsUsed());

    /// <summary>
    /// Выполнить событие по кнопке
    /// </summary>
    /// <param name="subAction">Дополнительное действие</param>
    public void Invoke(Action subAction = null)
    {
        subAction?.Invoke();
        OnButtonUse?.Invoke(this);
    }
    /// <summary>
    /// Выполнить событие по кнопке
    /// </summary>
    /// <param name="subAction">Дополнительное действие</param>
    /// <param name="condition">Условие</param>
    public void Invoke(Action subAction, bool condition = true)
    {
        if (condition)
            Invoke(subAction);
    }

    /// <returns>
    /// Кнопка нажата или удерживается
    /// </returns>
    private bool IsUsed()
    {
        if(IsButtonHold)
            return Input.GetKey(Button);

        return Input.GetKeyDown(Button);
    }
}