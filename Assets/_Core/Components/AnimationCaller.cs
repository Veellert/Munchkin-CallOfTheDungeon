using System;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за вызов анимаций
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimationCaller : MonoBehaviour
{
    /// <summary>
    /// Типы доступных анимаций
    /// </summary>
    private enum eAnimation
    {
        IDLE,
        RUNNING,
        DODGE,
        ATTACK,
        DIE,
    }

    private Animator _animator;

    private eAnimation _currentAnimation;
    private Action _finishAction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Играет анимацию смерти
    /// </summary>
    /// <param name="finishAction">Действие после конца анимации</param>
    public void DIE(Action finishAction = null)
    {
        Play(eAnimation.DIE, finishAction);
    }

    /// <summary>
    /// Играет анимацию атаки
    /// </summary>
    /// <param name="finishAction">Действие после конца анимации</param>
    public void ATTACK(Action finishAction = null)
    {
        Play(eAnimation.ATTACK, finishAction);
    }

    /// <summary>
    /// Играет анимацию рывка(кувырка)
    /// </summary>
    /// <param name="finishAction">Действие после конца анимации</param>
    public void DODGE(Action finishAction = null)
    {
        Play(eAnimation.DODGE, finishAction);
    }

    /// <summary>
    /// Играет анимацию спокойствия
    /// </summary>
    /// <param name="direction">Направление нужное для проверки</param>
    /// <param name="finishAction">Действие после конца анимации</param>
    public void IDLE(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.IDLE, finishAction, direction == Vector2.zero);
    }

    /// <summary>
    /// Играет анимацию бега
    /// </summary>
    /// <param name="direction">Направление нужное для проверки</param>
    /// <param name="finishAction">Действие после конца анимации</param>
    public void RUNNING(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.RUNNING, finishAction, direction != Vector2.zero);
    }

    /// <summary>
    /// Играет пустую анимацию, чтобы не нагружать систему
    /// </summary>
    public void Disabled() => _animator.Play("Disabled");

    /// <summary>
    /// Играет анимацию
    /// </summary>
    /// <param name="animation">Анимация</param>
    /// <param name="finishAction">Действие после конца анимации</param>
    /// <param name="condition">Условие</param>
    private void Play(eAnimation animation, Action finishAction, bool condition = true)
    {
        if (!condition || _currentAnimation == animation)
            return;

        _finishAction = finishAction;
        _currentAnimation = animation;

        _animator.Play(animation.ToString());
    }

    /// <summary>
    /// Метод который назначается в редакторе анимаций в Юнити
    /// </summary>
    /// <remarks>
    /// Используется для вызова функции по окончанию анимации
    /// </remarks>
    private void OnAnimationFinish() => _finishAction?.Invoke();
}
