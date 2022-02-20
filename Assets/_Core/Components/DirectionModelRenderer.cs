using System;
using UnityEngine;

/// <summary>
/// �������� �� ����������� ������
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
    /// �������������� ������� � ������������� ������ ������
    /// </summary>
    private void InitializeScales()
    {
        var scale = _model.transform.localScale;
        _scale = new Vector2(scale.x, scale.y);
        _scaleN = new Vector2(-scale.x, scale.y);
    }

    /// <summary>
    /// ��������� ����� �� ���������
    /// </summary>
    public void SubscribeChangingTo(ref Action<float> action) => action += ChangeDirection;

    /// <summary>
    /// ���������� �� ������ �� ���������
    /// </summary>
    public void UnsubscribeChangingForPlayer() => InputObserver.Instance.OnHorizontalAxisInput -= ChangeDirection;
    public void SubscribeChangingForPlayer() => InputObserver.Instance.OnHorizontalAxisInput += ChangeDirection;

    /// <summary>
    /// ������ ����������� � ����������� �� ���������� �����������
    /// </summary>
    /// <param name="direction">���������� �����������</param>
    private void ChangeDirection(float direction)
    {
        if (direction != 0)
            MirrorModel(direction < 0);
    }

    /// <summary>
    /// ������������� ������ � ����������� �� �������
    /// </summary>
    /// <param name="isLeft">������� ��� ������ ������� ��������������</param>
    private void MirrorModel(bool isLeft)
    {
        _model.transform.localScale = isLeft
            ? _scaleN
            : _scale;
    }
}
