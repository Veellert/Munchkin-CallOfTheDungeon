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

    private string _currentAnimation;
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
        TryStartAnimation(ANIMATION_IDLE, _movementDirection == Vector2.zero);
    }

    private void TryRun()
    {
        _rigBody.velocity = _movementDirection * _movementSpeed;
        TryStartAnimation(ANIMATION_RUNNING, _movementDirection != Vector2.zero);
    }

    #region Animations

    private readonly string[] ANIMATION_IDLE = { "Idle_F", "Idle_B", "Idle_LS", "Idle_RS" };
    private readonly string[] ANIMATION_RUNNING = { "Running_F", "Running_B", "Running_LS", "Running_RS" };

    private void TryStartAnimation(string[] animations, bool condition = true)
    {
        if (condition)
        {
            string animation = animations[(int)_directionManager.CurrentDirection];

            if (_currentAnimation == animation)
                return;

            _animator.Play(animation);
            _currentAnimation = animation;
        }
    }

    #endregion
}
