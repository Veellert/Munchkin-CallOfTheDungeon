using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eAnimation
{
    IDLE,
    RUNNING,
}

public class AnimationCaller
{
    #region Animations

    public class AnimationObject
    {
        public eAnimation Animation { get; set; }
        public string AnimationText => Animation.ToString();

        public eDirectionStatements Side { get; set; }
        public string SideText => $"{Side.ToString()[0]}";

        public string Name => AnimationText + "_" + SideText;

        public AnimationObject()
        {
            Animation = eAnimation.IDLE;
            Side = eDirectionStatements.Right;
        }

        public AnimationObject(eAnimation animation, eDirectionStatements side)
        {
            Animation = animation;
            Side = side;
        }
        
        public AnimationObject(eAnimation animation, int side)
        {
            Animation = animation;

            if (side == 0 || side == 1)
                Side = (eDirectionStatements)side;
            else
                Side = eDirectionStatements.Right;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return ((AnimationObject)obj).Name == Name;
        }

        public override int GetHashCode()
        {
            return Animation.GetHashCode() ^ Side.GetHashCode();
        }
    }

    private static AnimationObject GetAnimation(eAnimation animation, eDirectionStatements side)
    {
        return ANIMATIONS.Find(s => s.Animation == animation && s.Side == side);
    }

    #endregion

    private static readonly List<AnimationObject> ANIMATIONS = new List<AnimationObject>()
    {
        new AnimationObject(eAnimation.IDLE, 0),
        new AnimationObject(eAnimation.IDLE, 1),
        new AnimationObject(eAnimation.RUNNING, 0),
        new AnimationObject(eAnimation.RUNNING, 1),
    };

    private AnimationObject _currentAnimation;
    public Animator CurrentAnimator { get; set; }

    public AnimationCaller(Animator animator)
    {
        CurrentAnimator = animator;
    }

    public void Play(DirectionStatementManager direction, eAnimation animation, bool condition = true)
    {
        if(condition && direction?.CurrentDirection)
        {
            var animObj = GetAnimation(animation, direction.eCurrentDirection);
            if (_currentAnimation == animObj)
                return;

            _currentAnimation = animObj;
            CurrentAnimator.Play(_currentAnimation.Name);
        }
    }
}
