using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (Player.Instance)
            Destroy(Player.Instance.gameObject);
    }

    public void MainMenuPlayButtonClick() => SceneLoader.Lobby();
}
