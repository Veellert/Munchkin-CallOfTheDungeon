using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Скрипт отвечающий за загрузку доступных сцен
/// </summary>
public static class SceneLoader
{
    /// <summary>
    /// Список названий всех досьупных сцен
    /// </summary>
    private enum eScenes
    {
        MainMenu,
        Level0,
        Level1,
        Level1Boss1,
    }

    /// <summary>
    /// Запускает комнату 1 босса 1 уровня 
    /// </summary>
    public static void FirstBoss() => LoadScene(eScenes.Level1Boss1);

    /// <summary>
    /// Запускает подземелье 1 уровня 
    /// </summary>
    public static void FirstLevel() => LoadScene(eScenes.Level1);

    /// <summary>
    /// Запускает лобби
    /// </summary>
    public static void Lobby() => LoadScene(eScenes.Level0);

    /// <summary>
    /// Запускает главное меню
    /// </summary>
    public static void MainMenu() => LoadScene(eScenes.MainMenu);

    /// <summary>
    /// Запускает сцену из параметра
    /// </summary>
    /// <param name="scene">Название сцены из списка всех доступных сцен</param>
    private static void LoadScene(eScenes scene) => SceneManager.LoadScene(scene.ToString());
}