using System;
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

    private void Awake()
    {
        if(_indicatorSprite != null && _indicatorColor != null)
            _indicatorSprite.color = _indicatorColor;

        if(_target != null)
        {
            _attrib = _target.GetComponent<IDamageable>();
            _attrib.HP.OnValueChanged += UpdateIndicator;
        }
    }

    private void UpdateIndicator(object sender, EventArgs e)
    {
        if (_indicator == null || _target == null)
            return;

        _indicator.localScale = new Vector2(_hpPercent, _indicator.localScale.y);
        if (_attrib.IsDead)
        {
            _indicator.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _target = null;
        }
    }
}
