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
    }

    private void FixedUpdate()
    {
        if(Player.Instance && Player.Instance.IsDead)
        {
            Disable();
            return;
        }

        transform.position = Input.mousePosition;
    }

    private void OnDestroy()
    {
        Disable();
    }
}
