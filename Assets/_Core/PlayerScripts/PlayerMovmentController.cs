using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovmentController : MonoBehaviour
{
    [Header("RigidBody")]
    [SerializeField] private Rigidbody2D _rigBody;

    [Header("Animator")]
    [SerializeField] private Animator _animator;
    
    [Header("Direction")]
    [SerializeField] private DirectionStatementManager _directionManager;

    [Header("Running Speed")]
    [SerializeField] private float _movementSpeed = 6;

    private AnimationCaller _animation;
    private Vector2 _movementDirection;

    private void Start()
    {
        _animation = new AnimationCaller(_animator);
    }

    private void Update()
    {
        SetDirectionInput();
    }

    private void FixedUpdate()
    {
        CheckDirection();
        TryRun();
    }

    private void SetDirectionInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        _movementDirection = new Vector2(xInput, yInput).normalized;
    }

    private void CheckDirection()
    {
        _directionManager.ChangeDirection(_movementDirection);
        _animation.Play(_directionManager, AnimationCaller.AnimationObject.eAnimation.IDLE, _movementDirection == Vector2.zero);
    }

    private void TryRun()
    {
        _rigBody.velocity = _movementDirection * _movementSpeed;
        _animation.Play(_directionManager, AnimationCaller.AnimationObject.eAnimation.RUNNING, _movementDirection != Vector2.zero);
    }
}
