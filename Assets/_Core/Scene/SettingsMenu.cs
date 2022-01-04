using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика меню настроек
/// </summary>
public class SettingsMenu : MenuTemplate
{
    /// <summary>
    /// Сохранение настроек по кнопке
    /// </summary>
    public void SettingsMenuApplyButtonClick() => Debug.Log("Настройки сохранены");
}
