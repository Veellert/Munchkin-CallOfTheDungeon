using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MotionMonsterAttrib))]
public class MotionMonster : UnitObject
{
    [HideInInspector] public MotionMonsterAttrib Attrib;

    private eState _state = eState.Default;
    private enum eState
    {
        Default,
        Chase,
    }

    private UnitAttrib _stayPointTime;
    private UnitAttrib _removePointTime;
    private Transform _movePoint;
    private Vector2 _startPoint;

    private Transform _chaseTarget;

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(Attrib.IdleMovmentRadius));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(Attrib.ChaseRadius));
    }

    #endregion

    private void Awake()
    {
        Attrib = GetComponent<MotionMonsterAttrib>();
    }

    protected override void Start()
    {
        base.Start();
        name = Attrib.Name;
        _stayPointTime = Attrib.StayPointTime;
        _removePointTime = Attrib.RemovePointTime;

        _chaseTarget = Player.Instance.transform;
        _movePoint = new GameObject("movePoint_" + name).transform;

        _startPoint = transform.position;
        InitPoint();
    }

    private void FixedUpdate()
    {
        TryChase();
        switch (_state)
        {
            case eState.Default:
                SetDirection(_movePoint.position);
                RunHandle(_movePoint.position, Attrib.Speed);
                StayOnPoint();
                break;

            case eState.Chase:
                SetDirection(_chaseTarget.position);
                ChaseHandle();
                break;
        }
    }

    protected override void SetDirection(Vector2 direction)
    {
        _movementDirection = Direction2D.GetDirectionTo(transform.position, direction);

        CheckDirection();
    }

    private void RunHandle(Vector2 targetPosition, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        base.RunHandle();
    }

    private void TryChase()
    {
        if (Vector2.Distance(transform.position, _chaseTarget.position) <= new TileHalf(Attrib.ChaseRadius))
            _state = eState.Chase;
        else
            _state = eState.Default;
    }

    private void ChaseHandle()
    {
        if (Vector2.Distance(transform.position, _chaseTarget.position) > new TileHalf(1.3f))
        {
            float speed = Attrib.Speed.Value + 1;
            RunHandle(_chaseTarget.position, speed);
        }
        else
            _animation.Play(eAnimation.IDLE);
    }

    private void StayOnPoint()
    {
        _removePointTime -= Time.deltaTime;
        if (Vector2.Distance(transform.position, _movePoint.position) < 0.2f)
        {
            if (_stayPointTime.IsValueEmpty())
                InitPoint();
            else
                _stayPointTime -= Time.deltaTime;
        }
        else if (_removePointTime.IsValueEmpty())
            InitPoint();
    }

    private void InitPoint()
    {
        _stayPointTime.FillToMax();
        _removePointTime.FillToMax();

        _movePoint.position = GetRandomPointPosition();
    }

    private Vector2 GetRandomPointPosition()
    {
        var radius = new TileHalf(Attrib.IdleMovmentRadius);
        float x = Random.Range(_startPoint.x - radius, _startPoint.x + radius);
        float y = Random.Range(_startPoint.y - radius, _startPoint.y + radius);

        return new Vector2(x, y);
    }
}
