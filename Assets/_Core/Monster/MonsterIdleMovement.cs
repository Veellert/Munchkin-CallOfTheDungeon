using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private DirectionStatementManager _directionManager;

    [SerializeField]
    private CircleCollider2D _movementZone;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _startWaitTime;
    [SerializeField]
    private float _startRemovePointTime;

    private AnimationManager _animation = new AnimationManager();
    private Transform _movePoint;
    private Vector2 _startPoint;
    private float _waitTime;
    private float _removePointTime;
    private Vector2 _movementDirection = Vector2.zero;

    private void Start()
    {
        _startPoint = transform.position + (Vector3)_movementZone.offset/4;

        InitPoint();
    }

    private void Update()
    {
        SetDirection();
    }

    private void FixedUpdate()
    {
        CheckDirection();
        TryRun();

        _removePointTime += Time.deltaTime;
        if (Vector2.Distance(transform.position, _movePoint.position) < 0.2f)
        {
            if (_waitTime <= 0)
            {
                InitPoint();
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }
        else if(_removePointTime >= _startRemovePointTime)
            InitPoint();
    }

    private void SetDirection()
    {
        if (transform.position.x > _movePoint.position.x)
            _movementDirection.x = -1;
        else if (transform.position.x < _movePoint.position.x)
            _movementDirection.x = 1;
        else if (transform.position.x == _movePoint.position.x)
            _movementDirection.x = 0;
    }

    private void CheckDirection()
    {
        _directionManager.ChangeDirection(_movementDirection);
        _animation.Play(_directionManager, AnimationManager._IDLE, _animator, _movementDirection.x == 0);
    }

    private void TryRun()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, _movePoint.position, _movementSpeed * Time.deltaTime);
        _animation.Play(_directionManager, AnimationManager._RUNNING, _animator, _movementDirection.x != 0);
    }

    private void InitPoint()
    {
        _waitTime = _startWaitTime;
        _removePointTime = 0;

        if (!_movePoint)
            _movePoint = new GameObject().transform;
        _movePoint.position = GetRandomPoint(_movementZone.radius);
    }

    private Vector2 GetRandomPoint(float radius)
    {
        radius /= 4;
        float x = Random.Range(_startPoint.x - radius, _startPoint.x + radius);
        float y = Random.Range(_startPoint.y - radius, _startPoint.y + radius);

        return new Vector2(x, y);
    }
}
