using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eAnimation
{
    IDLE,
    RUNNING,
    DODGE,
}

[RequireComponent(typeof(Animator))]
public class AnimationCaller : MonoBehaviour
{
    [SerializeField] private Animator _currentAnimator;
    private eAnimation _currentAnimation;
    private Action _finishAction;

    private void Start()
    {
        if (_currentAnimator == null)
            Debug.Log("Что-то не так");
    }

    public void Play(eAnimation animation, Action finishAction, bool condition = true)
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
