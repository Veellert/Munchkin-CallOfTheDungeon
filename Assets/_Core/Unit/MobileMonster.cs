using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMonster : Monster, IIdleMovable
{
    #region Attrib

    [SerializeField] private UnitAttrib _idleMovmentRadius = new UnitAttrib(2, 10);
    public UnitAttrib IdleMovmentRadius { get => _idleMovmentRadius; set => _idleMovmentRadius = value; }
    [SerializeField] private UnitAttrib _stayPointTime = new UnitAttrib(2, 5);
    public UnitAttrib StayPointTime { get => _stayPointTime; set => _stayPointTime = value; }
    [SerializeField] private UnitAttrib _removePointTime = 5;
    public UnitAttrib RemovePointTime { get => _removePointTime; set => _removePointTime = value; }

    #endregion

    private Transform _movePoint;
    private Vector2 _startPoint;

    protected override void Start()
    {
        base.Start();

        _movePoint = new GameObject("movePoint_" + name + GetHashCode()).transform;
        _startPoint = transform.position;

        InitPoint();
    }

    private void FixedUpdate()
    {
        CheckAttackCooldown();
        switch (_state)
        {
            case eState.Default:
                TryChase();
                SetDirection(_movePoint.position);
                MoveTo(_movePoint.position);
                StayOnPoint();
                break;

            case eState.Chase:
                TryChase();
                SetDirection(_chaseTarget.position);
                ChaseHandler();
                break;

            case eState.Attack:
                AttackHandler();
                break;
        }
    }

    public void StayOnPoint()
    {
        if (Vector2.Distance(transform.position, _movePoint.position) < 0.2f)
        {
            if(!IsInvoking("InitPoint"))
                Invoke("InitPoint", StayPointTime);
        }
        else
        {
            if (!IsInvoking("RemovePoint"))
                Invoke("RemovePoint", RemovePointTime);
        }
    }

    public void InitPoint()
    {
        _movePoint.position = GetRandomPointPosition();
    }

    public void RemovePoint()
    {
        if (!IsInvoking("InitPoint"))
            InitPoint();
    }

    private Vector2 GetRandomPointPosition()
    {
        var radius = new TileHalf(IdleMovmentRadius);
        float x = Random.Range(_startPoint.x - radius, _startPoint.x + radius);
        float y = Random.Range(_startPoint.y - radius, _startPoint.y + radius);

        return new Vector2(x, y);
    }

    #region Gizmos

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(IdleMovmentRadius));
    }

    #endregion
}
