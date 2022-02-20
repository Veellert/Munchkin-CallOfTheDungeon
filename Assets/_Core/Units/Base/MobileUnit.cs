using System;
using UnityEngine;

/// <summary>
/// ��������� �������� ���������� �� ������ ���������� �����
/// </summary>
[RequireComponent(typeof(DirectionModelRenderer))]
public abstract class MobileUnit : BaseUnit
{
    //===>> Attributes & Properties <<===\\

    public NumericAttrib Speed { get; protected set; }

    //===>> Components & Fields <<===\\

    protected DirectionModelRenderer _directionRenderer;

    /// <summary>
    /// ������� ��� ����� �����������
    /// </summary>
    protected event Action<float> OnDirectionChanged;

    protected Vector2 _movementDirection;
    protected Vector2 _lastMovementDirection;

    //===>> Important Methods <<===\\

    protected override void GetRequiredComponents()
    {
        base.GetRequiredComponents();

        _directionRenderer = GetComponent<DirectionModelRenderer>();
    }
    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        _lastMovementDirection = Vector2.right;
        Speed = _preferences.Speed;

        _directionRenderer.SubscribeChangingTo(ref OnDirectionChanged);
    }

    //===>> Public Methods <<===\\

    /// <returns>
    /// ������� ����������� ��������
    /// </returns>
    public Vector2 GetMovementDirection() => _movementDirection;
    /// <returns>
    /// ���������� ����������� ��������
    /// </returns>
    public Vector2 GetLastMovementDirection() => _lastMovementDirection;

    /// <summary>
    /// ������������� �����������
    /// </summary>
    /// <param name="direction">�����������</param>
    /// <param name="needAnimation">����� �� ��������</param>
    /// <param name="needRendering">����� �� ��������������� �����������</param>
    public void SetDirection(Vector2 direction, bool needAnimation = true, bool needRendering = true)
    {
        _movementDirection = direction;

        if (_movementDirection != Vector2.zero)
            _lastMovementDirection = _movementDirection;

        if (needAnimation)
            _animation.IDLE(_movementDirection);

        if (needRendering)
            VisualizeDirection(direction.x);
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// ������������� �������� ������������ ����������� ���� ��������� ���� �� ���
    /// </summary>
    /// <param name="direction">���������� �����������</param>
    protected void VisualizeDirection(float direction) => OnDirectionChanged?.Invoke(direction);
}