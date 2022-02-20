using UnityEngine;

/// <summary>
/// ��������� ���������� �� ������������� ���������� �����
/// </summary>
public class UnitIndicatorInitializer : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Parent for Indicator")]
    [SerializeField] private Transform _parent;
    [Header("Indicator Object")]
    [SerializeField] private MinimapIndicatorObject _indicator;
    [Header("Indicator Position")]
    [SerializeField] private Vector2 _indicatorOffset;

    //===>> Components & Fields <<===\\

    private GameObject _currentIndicator;

    //===>> Unity <<===\\

    private void Start()
    {
        if (_indicator && _parent)
            InstantiateIndicator();
    }

    //===>> Editor Methods <<===\\

    /// <summary>
    /// ������� ������� ���������
    /// </summary>
    public void ClearIndicator() => DestroyImmediate(_currentIndicator);

    //===>> Public Methods <<===\\

    /// <summary>
    /// ������������� ��������� �� ����� ������
    /// </summary>
    public void InstantiateIndicator()
    {
        _currentIndicator = Instantiate(_indicator, _parent.position + GetCorrectOffset(), Quaternion.identity, _parent).gameObject;
        _currentIndicator.name = "Indicator";

        Vector3 GetCorrectOffset() => _indicatorOffset * _parent.parent.localScale;
    }
}