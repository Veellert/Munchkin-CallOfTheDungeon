using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _indicator;
    [SerializeField] private SpriteRenderer _indicatorSprite;
    [SerializeField] private BaseUnitAttrib _unitAttrib;
    [SerializeField] private Color _indicatorColor;

    private float _hpPercent => _unitAttrib.HP.Value / _unitAttrib.HP.MaxValue;
    private float _lastValue;

    private void Awake()
    {
        _indicatorSprite.color = _indicatorColor;
    }

    private void FixedUpdate()
    {
        if (_indicator == null || _unitAttrib == null)
            return;

        if (_hpPercent != _lastValue)
        {
            _lastValue = _hpPercent;
            _indicator.localScale = new Vector2(_lastValue, _indicator.localScale.y);
        }
    }
}
