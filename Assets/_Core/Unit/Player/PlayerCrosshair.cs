using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику курсора
/// </summary>
public class PlayerCrosshair : MonoBehaviour
{
    public static PlayerCrosshair Instance { get; set; }

    /// <summary>
    /// Отключает игровой курсор
    /// </summary>
    public static void Disable() => Destroy(Instance.gameObject);

    /// <summary>
    /// Включает игровой курсор
    /// </summary>
    public static void Activate(PlayerCrosshair crosshair)
    {
        Instantiate(crosshair, Vector2.zero, Quaternion.identity);
        Cursor.visible = false;
    }

    private void Start()
    {
        #region PreLoad

        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        #endregion

        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if(Player.Instance && Player.Instance.IsDead)
        {
            Disable();
            return;
        }

        transform.position = Direction2D.GetMousePosition();
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
