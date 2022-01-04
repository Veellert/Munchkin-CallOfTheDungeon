using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        if(_isApplicationQuit)
            GetComponent<Button>().onClick.AddListener(Application.Quit);
        else if(_isLobbyQuit)
            GetComponent<Button>().onClick.AddListener(SceneLoader.Lobby);
        else
            GetComponent<Button>().onClick.AddListener(SceneLoader.MainMenu);
    }
}
