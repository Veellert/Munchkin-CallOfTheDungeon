using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    [SerializeField] private bool _isApplicationQuit = true;

    private void Start()
    {
        if(_isApplicationQuit)
            GetComponent<Button>().onClick.AddListener(Application.Quit);
        else    
            GetComponent<Button>().onClick.AddListener(SceneLoader.MainMenu);
    }
}
