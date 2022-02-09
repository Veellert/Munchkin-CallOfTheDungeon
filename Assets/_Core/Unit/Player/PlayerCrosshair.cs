using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику курсора
/// </summary>
public class PlayerCrosshair : MonoBehaviour
{
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

    private void HP_OnValueChanged(object sender, System.EventArgs e)
    {
        if (((UnitAttrib)sender).IsValueEmpty())
            Disable();
    }
}
