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

    private AnimationManager _animation = new AnimationManager();
    private Vector2 _movementDirection;

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
        _animation.Play(_directionManager, AnimationManager._IDLE, _animator, _movementDirection == Vector2.zero);
    }

    private void TryRun()
    {
        _rigBody.velocity = _movementDirection * _movementSpeed;
        _animation.Play(_directionManager, AnimationManager._RUNNING, _animator, _movementDirection != Vector2.zero);
    }
}
