using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// “ипы доступных анимаций
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
///  омпонент отвечающий за вызов анимаций
/// </summary>
/// <remarks>
/// ƒл€ работы требует об€зательный компонент <see cref="Animator"/>
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
    /// »грает анимацию смерти
    /// </summary>
    /// <param name="finishAction">действие после конца анимации</param>
    public void PlayDIE(Action finishAction = null)
    {
        Play(eAnimation.DIE, finishAction);
    }

    /// <summary>
    /// »грает анимацию атаки
    /// </summary>
    /// <param name="finishAction">действие после конца анимации</param>
    public void PlayATTACK(Action finishAction = null)
    {
        Play(eAnimation.ATTACK, finishAction);
    }

    /// <summary>
    /// »грает анимацию рывка(кувырка)
    /// </summary>
    /// <param name="finishAction">действие после конца анимации</param>
    public void PlayDODGE(Action finishAction = null)
    {
        Play(eAnimation.DODGE, finishAction);
    }

    /// <summary>
    /// »грает анимацию спокойстви€
    /// </summary>
    /// <param name="direction">Ќаправление нужное дл€ проверки</param>
    /// <param name="finishAction">действие после конца анимации</param>
    public void PlayIDLE(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.IDLE, finishAction, direction == Vector2.zero);
    }

    /// <summary>
    /// »грает анимацию бега
    /// </summary>
    /// <param name="direction">Ќаправление нужное дл€ проверки</param>
    /// <param name="finishAction">действие после конца анимации</param>
    public void PlayRUNNING(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.RUNNING, finishAction, direction != Vector2.zero);
    }

    /// <summary>
    /// »грает пустую анимацию, чтобы не нагружать систему
    /// </summary>
    public void Disabled()
    {
        _animator.Play("Disabled");
    }

    /// <summary>
    /// »грает анимацию по заданным параметрам
    /// </summary>
    /// <param name="animation">анимаци€ из <see cref="eAnimation"/></param>
    /// <param name="finishAction">действие после конца анимации</param>
    /// <param name="condition">условие дл€ успешного выполнени€</param>
    private void Play(eAnimation animation, Action finishAction, bool condition = true)
    {
        // ѕроверка на условие и на совпадение анимаций
        if (!condition || _currentAnimation == animation)
            return;
        _finishAction = finishAction;

        _currentAnimation = animation;
        _animator.Play(animation.ToString());
    }

    /// <summary>
    /// ћетод который назначаетс€ в редакторе анимаций в ёнити
    /// </summary>
    private void OnAnimationFinish()
    {
        _finishAction?.Invoke();
    }
}
