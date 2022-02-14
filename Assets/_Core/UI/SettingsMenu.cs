using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    /// <summary>
    /// Логика меню настроек
    /// </summary>
    public class SettingsMenu : MenuTemplate
    {
        /// <summary>
        /// Сохранение настроек по кнопке
        /// </summary>
        public void SettingsMenuApplyButtonClick() => Debug.Log("Настройки сохранены");

        /// <summary>
        /// Смена разрешения по кнопке
        /// </summary>
        public void SettingsMenuChangeFullScreenButtonClick() => Screen.fullScreen = !Screen.fullScreen;
    }
}
