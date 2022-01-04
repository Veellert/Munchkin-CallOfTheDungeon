using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Типы доступных направлений для моделей
/// </summary>
public enum eDirectionStatement
{
    Right,
    Left,
}

/// <summary>
/// Менеджер по направлению модели
/// </summary>
public class DirectionStatementManager : MonoBehaviour
{
    /// <summary>
    /// Костная модель
    /// </summary>
    [SerializeField] private GameObject _model;
    [SerializeField] private eDirectionStatement _currentDirection;

    private Vector2 _scale;
    private Vector2 _scaleN;

    private void Start()
    {
        // Назначаю обычный и отрицательный размер
        var scale = _model.transform.localScale;
        _scale = new Vector2(scale.x, scale.y);
        _scaleN = new Vector2(-scale.x, scale.y);
    }

    /// <returns>
    /// Текущее направление
    /// </returns>
    public eDirectionStatement GetCurrentDirection() => _currentDirection;

    /// <summary>
    /// Меняет направление в зависимости от координаты направления
    /// </summary>
    /// <param name="direction">Координата направления</param>
    public void ChangeDirection(Vector2 direction)
    {
        if (direction.x < 0)
            ChangeDirectionState(eDirectionStatement.Left);
        else if(direction.x > 0)
            ChangeDirectionState(eDirectionStatement.Right);
    }

    /// <summary>
    /// Меняет направление модели в заданном направлении
    /// </summary>
    /// <param name="direction">Тип направления (право или лево)</param>
    private void ChangeDirectionState(eDirectionStatement direction)
    {
        if (_currentDirection == direction)
            return;

        _currentDirection = direction;
        // Отзеркаливает модель взависимости от направления
        _model.transform.localScale = _currentDirection == eDirectionStatement.Left ? _scaleN : _scale;
    }
}
