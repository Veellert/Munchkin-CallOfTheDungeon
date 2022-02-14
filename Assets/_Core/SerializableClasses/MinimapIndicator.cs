using Assets.UI;
using System;
using UnityEngine;

/// <summary>
/// Типы индикаторов для миникарты
/// </summary>
public enum eMinimapIndicator
{
    Player,
    Monster,
    Boss,
    Finish,
}

/// <summary>
/// Индикатор для миникарты
/// </summary>
[Serializable]
public struct MinimapIndicator
{
    [Header("Indicator Position")]
    [SerializeField] private Vector2 _position;
    public Vector2 Position { get => _position; private set => _position = value; }

    [Header("Indicator Type")]
    [SerializeField] private eMinimapIndicator _indicator;
    public eMinimapIndicator Indicator { get => _indicator; private set => _indicator = value; }

    public MinimapIndicator(Vector2 position, eMinimapIndicator indicator)
    {
        _position = position;
        _indicator = indicator;
    }

    /// <summary>
    /// Устанавливает индикатор на миникарту
    /// </summary>
    /// <param name="minimap">Миникарта для установки индикатора</param>
    public void SetIndicator(MinimapRenderer minimap) => minimap.SetIndicator(this);
    /// <summary>
    /// Устанавливает индикатор на миникарту <see cref = "MinimapRenderer.Instance" />
    /// </summary>
    public void SetIndicator() => SetIndicator(MinimapRenderer.Instance);
}