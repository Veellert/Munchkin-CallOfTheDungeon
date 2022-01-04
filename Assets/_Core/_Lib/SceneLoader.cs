using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������ ���������� �� �������� ��������� ����
/// </summary>
public static class SceneLoader
{
    /// <summary>
    /// ������ �������� ���� ��������� ����
    /// </summary>
    private enum eScenes
    {
        MainMenu,
        Level0,
        Level1,
        Level1Boss1,
    }

    /// <summary>
    /// ��������� ������� 1 ����� 1 ������ 
    /// </summary>
    public static void FirstBoss() => LoadScene(eScenes.Level1Boss1);

    /// <summary>
    /// ��������� ���������� 1 ������ 
    /// </summary>
    public static void FirstLevel() => LoadScene(eScenes.Level1);

    /// <summary>
    /// ��������� �����
    /// </summary>
    public static void Lobby() => LoadScene(eScenes.Level0);

    /// <summary>
    /// ��������� ������� ����
    /// </summary>
    public static void MainMenu() => LoadScene(eScenes.MainMenu);

    /// <summary>
    /// ��������� ����� �� ���������
    /// </summary>
    /// <param name="scene">�������� ����� �� ������ ���� ��������� ����</param>
    private static void LoadScene(eScenes scene) => SceneManager.LoadScene(scene.ToString());
}