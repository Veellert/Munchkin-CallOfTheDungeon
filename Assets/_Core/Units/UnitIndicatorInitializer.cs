using UnityEngine;

/// <summary>
/// Компонент отвечающий за инициализацию индикатора юнита
/// </summary>
public class UnitIndicatorInitializer : MonoBehaviour
{
    [Header("Parent for Indicator")]
    [SerializeField] private Transform _parent;
    [Header("Indicator Object")]
    [SerializeField] private MinimapIndicatorObject _indicator;
    [Header("Indicator Position")]
    [SerializeField] private Vector2 _indicatorOffset;

    private GameObject _current;

    private void Start()
    {
        if (_indicator && _parent)
            InstantiateIndicator();
    }

    /// <summary>
    /// Устанваливает индикатор на место модели
    /// </summary>
    public void InstantiateIndicator()
    {
        _current = Instantiate(_indicator, (Vector2)_parent.position + _indicatorOffset * _parent.parent.localScale, Quaternion.identity, _parent).gameObject;
        _current.name = "Indicator";
    }

    /// <summary>
    /// Удаляет поставленный индикатор
    /// </summary>
    public void ClearIndicator() => DestroyImmediate(_current);
}