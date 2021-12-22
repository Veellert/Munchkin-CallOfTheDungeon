using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(DirectionStatementManager))]
public abstract class UnitObject : MonoBehaviour
{
    protected Rigidbody2D _rigBody;
    protected DirectionStatementManager _directionManager;
    protected AnimationCaller _animation;

    protected Vector2 _movementDirection;
    protected Vector2 _lastMovementDirection;

    protected virtual void Start()
    {
        _rigBody = GetComponent<Rigidbody2D>();
        _directionManager = GetComponent<DirectionStatementManager>();
        _animation = new AnimationCaller(GetComponent<Animator>());
    }

    protected void Move(Vector2 direction) => _rigBody.velocity = direction;

    protected void CheckDirection()
    {
        _directionManager.ChangeDirection(_movementDirection);
        _animation.Play(eAnimation.IDLE, _movementDirection == Vector2.zero);
    }

    protected virtual void RunHandle()
    {
        _animation.Play(eAnimation.RUNNING, _movementDirection != Vector2.zero);
    }

    protected abstract void SetDirection(Vector2 direction);
}
