using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public void CancelButtonClick() => SceneLoader.MainMenu();

    public void StartButtonClick() => SceneLoader.FirstLevel();
}
