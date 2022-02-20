using System;
using UnityEngine;

/// <summary>
/// Менеджер по направлению модели
/// </summary>
public class DirectionModelRenderer : MonoBehaviour
{
    [SerializeField] private Transform _model;

    private Vector2 _scale;
    private Vector2 _scaleN;

    private void Start()
    {
        InitializeScales();

        ChangeDirection(1);
    }

    /// <summary>
    /// Инициализирует обычный и отрицательный размер модели
    /// </summary>
    private void InitializeScales()
    {
        var scale = _model.transform.localScale;
        _scale = new Vector2(scale.x, scale.y);
        _scaleN = new Vector2(-scale.x, scale.y);
    }

    /// <summary>
    /// Подписать метод на изменение
    /// </summary>
    public void SubscribeChangingTo(ref Action<float> action) => action += ChangeDirection;

    /// <summary>
    /// Отписаться от метода на изменение
    /// </summary>
    public void UnsubscribeChangingForPlayer() => InputObserver.Instance.OnHorizontalAxisInput -= ChangeDirection;
    public void SubscribeChangingForPlayer() => InputObserver.Instance.OnHorizontalAxisInput += ChangeDirection;

    /// <summary>
    /// Меняет направление в зависимости от координаты направления
    /// </summary>
    /// <param name="direction">Координата направления</param>
    private void ChangeDirection(float direction)
    {
        if (direction != 0)
            MirrorModel(direction < 0);
    }

    /// <summary>
    /// Отзеркаливает модель в зависимости от условия
    /// </summary>
    /// <param name="isLeft">Условие для выбора стороны отзеркаливания</param>
    private void MirrorModel(bool isLeft)
    {
        _model.transform.localScale = isLeft
            ? _scaleN
            : _scale;
    }
}
