using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionStatementManager))]
public class MonsterIdleMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private float _movementRadius;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _startWaitTime;
    [SerializeField] private float _startRemovePointTime;

    [SerializeField] private float _detectionRadius;

    private AnimationCaller _animation;
    private DirectionStatementManager _directionManager;

    private Transform _chaseTarget;
    private bool _isChasing => Vector2.Distance(transform.position, _chaseTarget.position) <= new TileHalf(_detectionRadius);

    private Vector2 _movementDirection = Vector2.zero;

    private Transform _movePoint;
    private Vector2 _startPoint;
    private float _waitTime;
    private float _removePointTime;

    private void Start()
    {
        _directionManager = GetComponent<DirectionStatementManager>();
        _animation = new AnimationCaller(_animator);
        _startPoint = transform.position;

        _chaseTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        InitPoint();
    }

    private void Update()
    {
        if (_isChasing)
            SetDirection(_chaseTarget);
        else
            SetDirection(_movePoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(_movementRadius));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(_detectionRadius));
    }

    private void FixedUpdate()
    {
        CheckDirection();

        if (_isChasing)
            TryRun(_chaseTarget);
        else
        {
            TryRun(_movePoint);

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
            else if (_removePointTime >= _startRemovePointTime)
                InitPoint();
        }
    }

    private void SetDirection(Transform target)
    {
        if (transform.position.x > target.position.x)
            _movementDirection.x = -1;
        else if (transform.position.x < target.position.x)
            _movementDirection.x = 1;
        else if (transform.position.x == target.position.x)
            _movementDirection.x = 0;
    }

    private void CheckDirection()
    {
        _directionManager.ChangeDirection(_movementDirection);
        _animation.Play(_directionManager, eAnimation.IDLE, _movementDirection.x == 0);
    }

    private void TryRun(Transform target)
    {
        if (_isChasing)
        {
            if(Vector2.Distance(transform.position, _chaseTarget.position) > new TileHalf(1.3f))
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, (_movementSpeed + 1) * Time.deltaTime);
                _animation.Play(_directionManager, eAnimation.RUNNING, _movementDirection.x != 0);
            }
            else
                _animation.Play(_directionManager, eAnimation.IDLE);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _movementSpeed * Time.deltaTime);
            _animation.Play(_directionManager, eAnimation.RUNNING, _movementDirection.x != 0);
        }
    }

    private void InitPoint()
    {
        _waitTime = _startWaitTime;
        _removePointTime = 0;

        if (!_movePoint)
            _movePoint = new GameObject().transform;
        _movePoint.position = GetRandomPoint(_movementRadius);
    }

    private Vector2 GetRandomPoint(float radius)
    {
        radius = new TileHalf(radius);
        float x = Random.Range(_startPoint.x - radius, _startPoint.x + radius);
        float y = Random.Range(_startPoint.y - radius, _startPoint.y + radius);

        return new Vector2(x, y);
    }
}
