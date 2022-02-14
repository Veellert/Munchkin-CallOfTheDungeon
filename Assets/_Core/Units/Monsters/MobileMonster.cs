using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику подвижного монстра
/// </summary>
public class MobileMonster : Monster, IIdleMovable
{
    [Header("IdleMovable Attribs")]
    [SerializeField] private NumericAttrib _idleMovmentRadius = new NumericAttrib(2, 10);
    public NumericAttrib IdleMovmentRadius { get => _idleMovmentRadius; private set => _idleMovmentRadius = value; }
    [SerializeField] private NumericAttrib _stayPointTime = new NumericAttrib(2, 5);
    public NumericAttrib StayPointTime { get => _stayPointTime; private set => _stayPointTime = value; }
    [SerializeField] private NumericAttrib _removePointTime = 5;
    public NumericAttrib RemovePointTime { get => _removePointTime; private set => _removePointTime = value; }


    private Vector2 _movePoint;
    private Vector2 _startPoint;

    protected override void Start()
    {
        base.Start();

        _startPoint = _movePoint = transform.position;
        InitPoint();
    }

    public void StayOnPoint()
    {
        var (methodName, delay) = GetInvokeInfo(Vector2.Distance(transform.position, _movePoint) < 0.2f);
        InvokeLoop(methodName, delay);

        (string methodName, float delay) GetInvokeInfo(bool condition)
        {
            if (condition)
                return (nameof(InitPoint), StayPointTime);

            return (nameof(RemovePoint), RemovePointTime);
        }
    }
    public void InitPoint()
    {
        _movePoint = GetRandomPointPosition();
    }
    public void RemovePoint()
    {
        InvokeLoop(nameof(InitPoint), InitPoint);
    }

    /// <returns>
    /// Рандомная координата для точки
    /// </returns>
    private Vector2 GetRandomPointPosition()
    {
        return new Vector2(GetRandomValue(_startPoint.x), GetRandomValue(_startPoint.y));

        float GetRandomValue(float value)
            => Random.Range(value - new TileHalf(IdleMovmentRadius), value + new TileHalf(IdleMovmentRadius));
    }

    protected override void InitializeStates()
    {
        base.InitializeStates();

        StateMachine.InitializeState(_defaultState, onExecute: ExecuteDefault);
        StateMachine.InitializeState(_attackState, onExecute: ExecuteAttack);
    }

    /// <summary>
    /// Логика атаки
    /// </summary>
    private void ExecuteAttack()
    {
        AttackHandler();
    }
    /// <summary>
    /// Обычная логика
    /// </summary>
    private void ExecuteDefault()
    {
        SetDirectionTo(_movePoint);
        MoveTo(_movePoint);
        StayOnPoint();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, new TileHalf(IdleMovmentRadius));
    }
}
