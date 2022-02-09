using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за загрузку уровня
/// </summary>
public class LevelInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private PlayerCamera _cameraInstance;

    [Header("Overlay")]
    [SerializeField] private PlayerOverlayDisplayer _overlayInstance;

    [Header("Additional Minimap Indicators")]
    [SerializeField] private List<MinimapIndicator> _minimapIndicatorList = new List<MinimapIndicator>();

    private void Start()
    {
        Instantiate(_cameraInstance);
        Instantiate(_overlayInstance);

        foreach (var minimapIndicator in _minimapIndicatorList)
            minimapIndicator.SetIndicator();
    }
}
