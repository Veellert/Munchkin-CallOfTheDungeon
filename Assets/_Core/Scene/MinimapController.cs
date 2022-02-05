using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public static MinimapController Instance { get; set; }
    public EventHandler OnActiveChanged { get; set; }
    public Camera Self { get; set; }

    private bool _isActive = true;

    private void Start()
    {
        Instance = this;
        Instance.Self = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnActiveChanged?.Invoke(null, EventArgs.Empty);
            _isActive = !_isActive;
        }

        if (_isActive)
        {
            if (Input.GetKey(KeyCode.Minus) && Instance.Self.orthographicSize > 5)
                Instance.Self.orthographicSize--;
            else if (Input.GetKey(KeyCode.Equals) && Instance.Self.orthographicSize < 15)
                Instance.Self.orthographicSize ++;
        }
    }
}
