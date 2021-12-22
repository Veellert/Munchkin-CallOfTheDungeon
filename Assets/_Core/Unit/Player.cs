using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttrib))]
public class Player : UnitObject
{
    [HideInInspector] public PlayerAttrib Attrib;

    private eState _state = eState.Default;
    private enum eState
    {
        Default,
        Dodge,
    }

    private UnitAttrib _dodgeForce;
    private UnitAttrib _dodgeCooldown;

    #region Instance

    public static Player Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Attrib = GetComponent<PlayerAttrib>();
    }

    #endregion

    protected override void Start()
    {
        base.Start();
        name = Attrib.Name;
        _dodgeForce = Attrib.DodgeForce.Value;
        _dodgeCooldown = Attrib.DodgeCooldown.Value;
    }

    private void FixedUpdate()
    {
        CheckDodgeCooldown();
        switch (_state)
        {
            case eState.Default:
                SetDirection(GetInputDirection());
                InputHandle();
                RunHandle();
                break;

            case eState.Dodge:
                TryDodge();
                break;
        }

    }

    protected override void SetDirection(Vector2 direction)
    {
        _movementDirection = direction;
        if (_movementDirection != Vector2.zero)
            _lastMovementDirection = _movementDirection;

        CheckDirection();
    }

    protected override void RunHandle()
    {
        Move(_movementDirection* Attrib.Speed);
        base.RunHandle();
    }

    private void InputHandle()
    {
        if (Input.GetKey(KeyCode.Space) && _dodgeCooldown.IsValueEmpty())
        {
            _dodgeCooldown.FillToMax();
            _dodgeForce.FillToMax();
            _state = eState.Dodge;
        }
    }

    private void TryDodge()
    {
        if (_lastMovementDirection == Vector2.zero)
            _lastMovementDirection = Vector2.right;

        Move(_lastMovementDirection * _dodgeForce);
        _dodgeForce -= _dodgeForce.MaxValue * Time.deltaTime;

        _animation.Play(eAnimation.DODGE);

        if (_dodgeForce < 1)
            _state = eState.Default;
    }

    private void CheckDodgeCooldown()
    {
        if (_dodgeCooldown > 0)
            _dodgeCooldown -= Time.deltaTime;
        if (_dodgeCooldown < 0)
            _dodgeCooldown = 0;
    }

    private Vector2 GetInputDirection()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        return new Vector2(xInput, yInput).normalized;
    }
}
