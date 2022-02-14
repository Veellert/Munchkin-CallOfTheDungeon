using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.UI
{
    /// <summary>
    /// Компонент для кнопки выхода
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class QuitButton : MonoBehaviour
    {
        [SerializeField] private bool _isApplicationQuit = true;
        [SerializeField] private bool _isLobbyQuit = false;

        private void Start()
        {
            if (_isApplicationQuit && _isLobbyQuit)
                _isLobbyQuit = false;

            if (_isApplicationQuit)
                InitializeButtonClick(Application.Quit);
            else if (_isLobbyQuit)
                InitializeButtonClick(SceneLoader.Lobby);
            else
                InitializeButtonClick(SceneLoader.MainMenu);
        }

        /// <summary>
        /// Инициализирует действие для клика по кнопке
        /// </summary>
        /// <param name="click">Действие по клику</param>
        private void InitializeButtonClick(UnityAction click)
        {
            GetComponent<Button>().onClick.AddListener(click);
        }
    }
}
