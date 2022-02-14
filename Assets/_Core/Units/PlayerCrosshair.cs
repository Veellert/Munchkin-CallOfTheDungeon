using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику курсора
/// </summary>
public class PlayerCrosshair : MonoBehaviour
{
    private void Start()
    {
        Activate();
        Player.Instance.HP.OnValueChanged += HP_OnValueChanged;
    }

    private void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }

    private void OnDestroy()
    {
        Disable();
        Player.Instance.HP.OnValueChanged -= HP_OnValueChanged;
    }

    /// <summary>
    /// Отключает игровой курсор
    /// </summary>
    public void Disable()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Включает игровой курсор
    /// </summary>
    public void Activate()
    {
        Cursor.visible = false;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Событие при изменении здоровья
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void HP_OnValueChanged(NumericAttrib hp)
    {
        if (hp.IsValueEmpty())
            Disable();
    }
}
