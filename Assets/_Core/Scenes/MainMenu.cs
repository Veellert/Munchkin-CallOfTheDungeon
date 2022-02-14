using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Компонент отвечающий за логику главного меню
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
}
