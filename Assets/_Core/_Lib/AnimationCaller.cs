using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��������� ��������
/// </summary>
public enum eAnimation
{
    IDLE,
    RUNNING,
    DODGE,
    ATTACK,
    DIE,
}

/// <summary>
/// ��������� ���������� �� ����� ��������
/// </summary>
/// <remarks>
/// ��� ������ ������� ������������ ��������� <see cref="Animator"/>
/// </remarks>
[RequireComponent(typeof(Animator))]
public class AnimationCaller : MonoBehaviour
{
    private Animator _animator;
    private eAnimation _currentAnimation;
    private Action _finishAction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// ������ �������� ������
    /// </summary>
    /// <param name="finishAction">�������� ����� ����� ��������</param>
    public void PlayDIE(Action finishAction = null)
    {
        Play(eAnimation.DIE, finishAction);
    }

    /// <summary>
    /// ������ �������� �����
    /// </summary>
    /// <param name="finishAction">�������� ����� ����� ��������</param>
    public void PlayATTACK(Action finishAction = null)
    {
        Play(eAnimation.ATTACK, finishAction);
    }

    /// <summary>
    /// ������ �������� �����(�������)
    /// </summary>
    /// <param name="finishAction">�������� ����� ����� ��������</param>
    public void PlayDODGE(Action finishAction = null)
    {
        Play(eAnimation.DODGE, finishAction);
    }

    /// <summary>
    /// ������ �������� �����������
    /// </summary>
    /// <param name="direction">����������� ������ ��� ��������</param>
    /// <param name="finishAction">�������� ����� ����� ��������</param>
    public void PlayIDLE(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.IDLE, finishAction, direction == Vector2.zero);
    }

    /// <summary>
    /// ������ �������� ����
    /// </summary>
    /// <param name="direction">����������� ������ ��� ��������</param>
    /// <param name="finishAction">�������� ����� ����� ��������</param>
    public void PlayRUNNING(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.RUNNING, finishAction, direction != Vector2.zero);
    }

    /// <summary>
    /// ������ ������ ��������, ����� �� ��������� �������
    /// </summary>
    public void Disabled()
    {
        _animator.Play("Disabled");
    }

    /// <summary>
    /// ������ �������� �� �������� ����������
    /// </summary>
    /// <param name="animation">�������� �� <see cref="eAnimation"/></param>
    /// <param name="finishAction">�������� ����� ����� ��������</param>
    /// <param name="condition">������� ��� ��������� ����������</param>
    private void Play(eAnimation animation, Action finishAction, bool condition = true)
    {
        // �������� �� ������� � �� ���������� ��������
        if (!condition || _currentAnimation == animation)
            return;
        _finishAction = finishAction;

        _currentAnimation = animation;
        _animator.Play(animation.ToString());
    }

    /// <summary>
    /// ����� ������� ����������� � ��������� �������� � �����
    /// </summary>
    private void OnAnimationFinish()
    {
        _finishAction?.Invoke();
    }
}
