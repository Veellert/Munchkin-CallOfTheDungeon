using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverlayInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _overlay;

    private void Start()
    {
        if (_overlay) Instantiate(_overlay, gameObject.transform);
    }
}
