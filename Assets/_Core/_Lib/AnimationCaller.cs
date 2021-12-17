using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eAnimation
{
    IDLE,
    RUNNING,
    DODGE,
}

public class AnimationCaller
{
    private Animator CurrentAnimator { get; set; }
    private eAnimation _currentAnimation;

    public AnimationCaller(Animator animator)
    {
        CurrentAnimator = animator;
    }

    public void Play(eAnimation animation, bool condition = true)
    {
        if (!condition || _currentAnimation == animation)
            return;

        _currentAnimation = animation;
        CurrentAnimator.Play(animation.ToString());
    }
}
