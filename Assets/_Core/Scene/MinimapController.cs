using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MinimapIndicator
{
    [SerializeField] private Vector2 _indicatorPosition;
    public Vector2 IndicatorPosition
    {
        get { return _indicatorPosition; }
        set { _indicatorPosition = value; }
    }

    [SerializeField] private eMinimapIndicator _indicator;
    public eMinimapIndicator Indicator
    {
        get { return _indicator; }
        set { _indicator = value; }
    }

    public MinimapIndicator(Vector2 position, eMinimapIndicator indicator)
    {
        _indicatorPosition = position;
        _indicator = indicator;
    }

    public void SetIndicator()
        => MinimapController.Instance?.SetIndicator(this);
}

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
    public Camera Self { get; set; }

    [SerializeField] private List<GameObject> _indicatorList = new List<GameObject>();

    private bool _isActive = true;

    private void Start()
    {
        Instance = this;
        Instance.Self = GetComponent<Camera>();

        InputObserver.Instance.minimap.OnButtonEvent += OnMinimap;
        InputObserver.Instance.minimapIncrease.OnButtonEvent += OnMinimapIncrease;
        InputObserver.Instance.minimapDecrease.OnButtonEvent += OnMinimapDecrease;
    }

    private void OnDestroy()
    {
        InputObserver.Instance.minimap.OnButtonEvent -= OnMinimap;
        InputObserver.Instance.minimapIncrease.OnButtonEvent -= OnMinimapIncrease;
        InputObserver.Instance.minimapDecrease.OnButtonEvent -= OnMinimapDecrease;
    }

    private void OnMinimap(object sender, EventArgs e)
    {
        _isActive = !_isActive;
    }
    
    private void OnMinimapIncrease(object sender, EventArgs e)
    {
        if (_isActive && Instance.Self.orthographicSize < 15)
            Instance.Self.orthographicSize += 0.5f;
    }
    
    private void OnMinimapDecrease(object sender, EventArgs e)
    {
        if (_isActive && Instance.Self.orthographicSize > 5)
            Instance.Self.orthographicSize -= 0.5f;
    }

    public void SetIndicator(MinimapIndicator minimapIndicator)
    {
        if (_indicatorList.Count < Enum.GetNames(typeof(eMinimapIndicator)).Length)
            return;

        var indicator = _indicatorList[(int)minimapIndicator.Indicator];
        Instantiate(indicator, minimapIndicator.IndicatorPosition, Quaternion.identity);
    }
}
