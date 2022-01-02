using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private enum eScenes
    {
        MainMenu,
        Level0,
        Level1,
        Level1Boss1,
    }

    public static void FirstBoss() => LoadScene(eScenes.Level1Boss1);
    
    public static void FirstLevel() => LoadScene(eScenes.Level1);

    public static void Lobby() => LoadScene(eScenes.Level0);

    public static void MainMenu() => LoadScene(eScenes.MainMenu);

    private static void LoadScene(eScenes scene) => SceneManager.LoadScene(scene.ToString());
}