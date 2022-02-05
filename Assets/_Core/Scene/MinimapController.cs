using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMinimapIndicator
{
    Player,
    Monster,
    Boss,
    Finish,
}

public class MinimapController : MonoBehaviour
{
    public static MinimapController Instance { get; set; }
    public EventHandler OnActiveChanged { get; set; }
    public Camera Self { get; set; }

    [SerializeField] private List<GameObject> _indicatorList = new List<GameObject>();

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
                Instance.Self.orthographicSize -= 0.5f;
            else if (Input.GetKey(KeyCode.Equals) && Instance.Self.orthographicSize < 15)
                Instance.Self.orthographicSize += 0.5f;
        }
    }

    public void SetIndicator(Vector2 position, eMinimapIndicator indicator)
    {
        if (_indicatorList.Count < Enum.GetNames(indicator.GetType()).Length)
            return;

        Instantiate(_indicatorList[(int)indicator], position, Quaternion.identity);
    }
}
