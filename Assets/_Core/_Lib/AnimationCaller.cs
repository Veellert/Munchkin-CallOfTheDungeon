using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eAnimation
{
    IDLE,
    RUNNING,
    DODGE,
    ATTACK,
    DIE,
}

[RequireComponent(typeof(Animator))]
public class AnimationCaller : MonoBehaviour
{
    private Animator _currentAnimator;
    private eAnimation _currentAnimation;
    private Action _finishAction;

    private void Awake()
    {
        _currentAnimator = GetComponent<Animator>();
    }

    public void PlayDIE(Action finishAction = null)
    {
        Play(eAnimation.DIE, finishAction);
    }
    
    public void PlayATTACK(Action finishAction = null)
    {
        Play(eAnimation.ATTACK, finishAction);
    }
    
    public void PlayDODGE(Action finishAction = null)
    {
        Play(eAnimation.DODGE, finishAction);
    }
    
    public void PlayIDLE(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.IDLE, finishAction, direction == Vector2.zero);
    }
    
    public void PlayRUNNING(Vector2 direction, Action finishAction = null)
    {
        Play(eAnimation.RUNNING, finishAction, direction != Vector2.zero);
    }

    private void Play(eAnimation animation, Action finishAction, bool condition = true)
    {
        if (!condition || _currentAnimation == animation)
            return;
        _finishAction = finishAction;

        _currentAnimation = animation;
        _currentAnimator.Play(animation.ToString());
    }

    private void OnAnimationFinish()
    {
        _finishAction?.Invoke();
    }
}
