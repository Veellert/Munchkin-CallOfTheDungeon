using System;
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
        Attack,
    }

    private UnitAttrib _dodgeForce;
    private UnitAttrib _dodgeCooldown;
    private UnitAttrib _attackCooldown;

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
        _attackCooldown = Attrib.AttackCooldown.Value;
    }

    private void FixedUpdate()
    {
        CheckDodgeCooldown();
        CheckAttackCooldown();
        switch (_state)
        {
            case eState.Default:
                SetDirection(GetInputDirection());
                InputHandle();
                RunHandle();
                AttackHandle();
                break;

            case eState.Dodge:
                TryDodge();
                break;

            case eState.Attack:
                AttackHandle();
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

    protected override void RunHandle(Action finishAction = null)
    {
        Move(_movementDirection* Attrib.Speed);
        base.RunHandle();
    }
    
    protected override void AttackHandle(Action finishAction = null)
    {
        if (Input.GetMouseButton(0) && _attackCooldown.IsValueEmpty())
        {
            var attackDirection = (GetMousePosition() - transform.position).normalized;
            _state = eState.Attack;
            SetDirection(Vector2.zero);
            _attackCooldown.FillToMax();
            base.AttackHandle(() => { _state = eState.Default; });
        }
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

    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    private void CheckAttackCooldown()
    {
        if (_attackCooldown > 0)
            _attackCooldown -= Time.deltaTime;
    }

    private void TryDodge()
    {
        if (_lastMovementDirection == Vector2.zero)
            _lastMovementDirection = Vector2.right;

        Move(_lastMovementDirection * _dodgeForce);
        _dodgeForce -= _dodgeForce.MaxValue * Time.deltaTime;

        _animation.Play(eAnimation.DODGE, () => { Debug.Log(Attrib.Name + "Додж"); });

        if (_dodgeForce < 1)
            _state = eState.Default;
    }

    private void CheckDodgeCooldown()
    {
        if (_dodgeCooldown > 0)
            _dodgeCooldown -= Time.deltaTime;
    }

    private Vector2 GetInputDirection()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        return new Vector2(xInput, yInput).normalized;
    }
}
