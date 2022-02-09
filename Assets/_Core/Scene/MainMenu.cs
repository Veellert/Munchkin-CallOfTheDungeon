using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика главного меню
/// </summary>
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (Player.Instance)
            Destroy(Player.Instance.gameObject);
        if (InputObserver.Instance)
            Destroy(InputObserver.Instance.gameObject);
    }

    /// <summary>
    /// Запуск игры по кнопке
    /// </summary>
    public void MainMenuPlayButtonClick() => SceneLoader.Lobby();
}
