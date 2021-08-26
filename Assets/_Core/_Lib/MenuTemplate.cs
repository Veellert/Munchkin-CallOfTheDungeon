using UnityEngine;
using UnityEngine.UI;

public class MenuTemplate : MonoBehaviour
{
    [SerializeField] private Button[] _activeButtons;

    private void Start()
    {
        foreach (var button in _activeButtons)
        {
            button.onClick.AddListener(InverseActive);
        }

        InverseActive();
    }

    private void InverseActive() => gameObject.SetActive(!gameObject.activeSelf);
}