using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    #region Animations

    public static readonly string[] _IDLE = { "Idle_F", "Idle_B", "Idle_LS", "Idle_RS" };
    public static readonly string[] _RUNNING = { "Running_F", "Running_B", "Running_LS", "Running_RS" };

    #endregion

    private string _currentAnimation;

    public void Play(DirectionStatementManager direction,
        string[] animations, Animator animator, bool condition = true)
    {
        if(condition && direction.GetCurrentDirectionStatement() != null)
        {
            string animation = animations[(int)direction.CurrentDirection];

            if (_currentAnimation == animation)
                return;

            animator.Play(animation);
            _currentAnimation = animation;
        }
    }
}
