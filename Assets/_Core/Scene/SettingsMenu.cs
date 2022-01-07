using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���� ��������
/// </summary>
public class SettingsMenu : MenuTemplate
{
    /// <summary>
    /// ���������� �������� �� ������
    /// </summary>
    public void SettingsMenuApplyButtonClick() => Debug.Log("��������� ���������");

    /// <summary>
    /// ����� ���������� �� ������
    /// </summary>
    public void SettingsMenuChangeFullScreenButtonClick() => Screen.fullScreen = !Screen.fullScreen;
}
