using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��������� ����������� ��� �������
/// </summary>
public enum eDirectionStatement
{
    Right,
    Left,
}

/// <summary>
/// �������� �� ����������� ������
/// </summary>
public class DirectionStatementManager : MonoBehaviour
{
    /// <summary>
    /// ������� ������
    /// </summary>
    [SerializeField] private GameObject _model;
    [SerializeField] private eDirectionStatement _currentDirection;

    private Vector2 _scale;
    private Vector2 _scaleN;

    private void Start()
    {
        // �������� ������� � ������������� ������
        var scale = _model.transform.localScale;
        _scale = new Vector2(scale.x, scale.y);
        _scaleN = new Vector2(-scale.x, scale.y);
    }

    /// <returns>
    /// ������� �����������
    /// </returns>
    public eDirectionStatement GetCurrentDirection() => _currentDirection;

    /// <summary>
    /// ������ ����������� � ����������� �� ���������� �����������
    /// </summary>
    /// <param name="direction">���������� �����������</param>
    public void ChangeDirection(Vector2 direction)
    {
        if (direction.x < 0)
            ChangeDirectionState(eDirectionStatement.Left);
        else if(direction.x > 0)
            ChangeDirectionState(eDirectionStatement.Right);
    }

    /// <summary>
    /// ������ ����������� ������ � �������� �����������
    /// </summary>
    /// <param name="direction">��� ����������� (����� ��� ����)</param>
    private void ChangeDirectionState(eDirectionStatement direction)
    {
        if (_currentDirection == direction)
            return;

        _currentDirection = direction;
        // ������������� ������ ������������ �� �����������
        _model.transform.localScale = _currentDirection == eDirectionStatement.Left ? _scaleN : _scale;
    }
}
