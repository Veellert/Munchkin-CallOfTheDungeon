using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _indicator;
    [SerializeField] private SpriteRenderer _indicatorSprite;
    [SerializeField] private Color _indicatorColor;

    private IDamageable _attrib;
    private float _hpPercent => _attrib.HP.Value / _attrib.HP.MaxValue;
    private float _lastValue;

    private void Awake()
    {
        _indicatorSprite.color = _indicatorColor;
        _attrib = _target.GetComponent<IDamageable>();
    }

    private void FixedUpdate()
    {
        if (_indicator == null || _target == null)
            return;

        if (_hpPercent != _lastValue)
        {
            _lastValue = _hpPercent;
            _indicator.localScale = new Vector2(_lastValue, _indicator.localScale.y);
        }

        if (_attrib.IsDead)
        {
            gameObject.SetActive(false);
            _target = null;
        }
    }
}
