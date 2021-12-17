using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DirectionStatementManager))]
public class PlayerMovmentController : MonoBehaviour
{
    [Header("RigidBody")]
    [SerializeField] private Rigidbody2D _rigBody;

    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _dodgeForce = 6;
    [SerializeField] private float _dodgeCooldown = 2;

    private DirectionStatementManager _directionManager;
    private AnimationCaller _animation;

    private Vector2 _movementDirection;
    private Vector2 _lastMovementDirection;
    private float _currentDodgeCooldown;

    private float _dodgeSpeed;
    private bool _isDodgeCooldown => _currentDodgeCooldown != 0;

    private eState _state = eState.Default;
    private enum eState
    {
        Default,
        Dodge,
    }

    private void Start()
    {
        _directionManager = GetComponent<DirectionStatementManager>();
        _animation = new AnimationCaller(_animator);
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case eState.Default:
                SetDirectionInput();
                CheckDirection();
                TryRun();
                break;
            case eState.Dodge:
                TryDodge();
                break;
        }
        
    }

    private void SetDirectionInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        _movementDirection = new Vector2(xInput, yInput).normalized;
        if (_movementDirection != Vector2.zero)
            _lastMovementDirection = _movementDirection;

        CheckDodgeCooldown();
        if (Input.GetKey(KeyCode.Space) && !_isDodgeCooldown)
        {
            _currentDodgeCooldown = _dodgeCooldown;
            _dodgeSpeed = _dodgeForce;
            _state = eState.Dodge;
        }
    }

    private void CheckDirection()
    {
        _directionManager.ChangeDirection(_movementDirection);
        _animation.Play(eAnimation.IDLE, _movementDirection == Vector2.zero);
    }

    private void TryRun()
    {
        Move(_movementDirection * _movementSpeed);
        _animation.Play(eAnimation.RUNNING, _movementDirection != Vector2.zero);
    }

    private void TryDodge()
    {
        CheckDodgeCooldown();

        if (_lastMovementDirection == Vector2.zero)
            _lastMovementDirection = Vector2.right;

        Move(_lastMovementDirection * _dodgeSpeed);
        _dodgeSpeed -= _dodgeForce * Time.deltaTime;

        _animation.Play(eAnimation.DODGE);

        if (_dodgeSpeed < 1)
            _state = eState.Default;
    }

    private void CheckDodgeCooldown()
    {
        if (_currentDodgeCooldown > 0)
            _currentDodgeCooldown -= Time.deltaTime;
        if (_currentDodgeCooldown < 0)
            _currentDodgeCooldown = 0;
    }

    private void Move(Vector2 vector)
    {
        _rigBody.velocity = vector;
    }
}
