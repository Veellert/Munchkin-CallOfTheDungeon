using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику полосы здоровья
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _indicator;
    [SerializeField] private SpriteRenderer _indicatorSprite;
    [SerializeField] private Color _indicatorColor;

    private IDamageable _attrib;
    private float HpPercent => _attrib.HP.Value / _attrib.HP.MaxValue;

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

    /// <summary>
    /// Обновляет индикатор
    /// </summary>
    private void UpdateIndicator(object sender, EventArgs e)
    {
        if (_indicator == null || _target == null)
            return;

        _indicator.localScale = new Vector2(HpPercent, _indicator.localScale.y);
        if (_attrib.IsDead)
        {
            Invoke("Destroy", 10);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Разрушение объекта
    /// </summary>
    private void Destroy()
    {
        Destroy(_target.gameObject);
    }
}
