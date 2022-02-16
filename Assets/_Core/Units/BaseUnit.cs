using System;
using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику юнита
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(DirectionStatementVisualizer), typeof(AnimationCaller))]
public abstract class BaseUnit : MonoBehaviour, IUnit
{
    [Header("Unit Preferences")]
    [SerializeField] protected UnitPreferences _preferences;

    [Header("Base Attribs")]
    [SerializeField] protected NumericAttrib _hitboxDistance;
    public NumericAttrib HitboxDistance { get => _hitboxDistance; protected set => _hitboxDistance = value; }
    [SerializeField] protected Vector2 _hitboxOffset;
    public Vector2 HitboxOffset { get => _hitboxOffset; protected set => _hitboxOffset = value; }

    [Header("Unit Attribs")]
    [SerializeField] protected NumericAttrib _speed = new NumericAttrib();
    public NumericAttrib Speed { get => _speed; protected set => _speed = value; }

    public UnitStateMachine StateMachine { get; private set; }


    protected readonly UnitState _defaultState = new UnitState("Default");


    /// <summary>
    /// Событие при смене направления
    /// </summary>
    protected event Action<float> OnDirectionChanged;

    protected Rigidbody2D _rigBody;
    protected AnimationCaller _animation;

    protected Vector2 _movementDirection;
    protected Vector2 _lastMovementDirection;

    protected Vector2 _tempAttackPosition;

    protected virtual void Start()
    {
        _rigBody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<AnimationCaller>();
        GetComponent<DirectionStatementVisualizer>().SubscribeChangingTo(ref OnDirectionChanged);

        name = _preferences.ID;
        StateMachine = new UnitStateMachine(_defaultState);

        GetRequiredComponents();
        SubscribeOnEvents();

        InitializeStates();
        StateMachine.Start();
    }

    /// <summary>
    /// Получает все требуемые компоненты
    /// </summary>
    protected abstract void GetRequiredComponents();
    /// <summary>
    /// Подписывает на события
    /// </summary>
    protected abstract void SubscribeOnEvents();
    /// <summary>
    /// Инициализирует состояния юнита
    /// </summary>
    protected abstract void InitializeStates();

    /// <summary>
    /// Проверка на кулдауны
    /// </summary>
    protected virtual void CheckAllCooldowns()
    {

    }

    /// <summary>
    /// Исполнить атаку
    /// </summary>
    /// <param name="target">Цель</param>
    protected virtual void ReleaseAttack(IDamageable target, float damage)
    {
        target?.GetDamage(damage);
    }

    /// <summary>
    /// Разрушение объекта юнита
    /// </summary>
    protected void DestroySelf()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Выполняет метод проверяя не выполняется ли он уже
    /// </summary>
    /// <param name="methodName">Название метода</param>
    /// <param name="delay">Задержка перед выполнением</param>
    protected void InvokeLoop(string methodName, float delay)
    {
        if (!IsInvoking(methodName))
            Invoke(methodName, delay);
    }
    /// <summary>
    /// Выполняет действие проверяя не выполняется ли какой-то метод
    /// </summary>
    /// <param name="methodName">Название метода</param>
    /// <param name="action">Действие</param>
    protected void InvokeLoop(string methodName, Action action)
    {
        if (!IsInvoking(methodName))
            action?.Invoke();
    }

    /// <summary>
    /// Принудительно изменяет визуализацию направления если изменение было из вне
    /// </summary>
    /// <param name="direction">Измененное направление</param>
    protected void VisualizeDirection(float direction) => OnDirectionChanged?.Invoke(direction);

    /// <summary>
    /// Устанавливает направление
    /// </summary>
    /// <param name="direction">Направление</param>
    /// <param name="needAnimation">Нужна ли анимация</param>
    protected void SetDirection(Vector2 direction, bool needAnimation = true)
    {
        _movementDirection = direction;
        if (_movementDirection != Vector2.zero)
            _lastMovementDirection = _movementDirection;

        if (needAnimation)
            _animation.IDLE(_movementDirection);
    }

    /// <summary>
    /// Движение к цели
    /// </summary>
    /// <param name="targetPosition">Цель</param>
    /// <param name="extraSpeed">Прибавка к скорости</param>
    protected void MoveTo(Vector2 targetPosition, float extraSpeed = 0)
    {
        float totalSpeed = (Speed.Value + extraSpeed) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, totalSpeed);
        _animation.RUNNING(_movementDirection);
    }
    /// <summary>
    /// Движение к цели
    /// </summary>
    /// <param name="targetPosition">Цель</param>
    /// <param name="finishFunction">Функция по окончании движения</param>
    /// <param name="extraSpeed">Прибавка к скорости</param>
    protected void MoveTo(Vector2 targetPosition, Action finishFunction, float extraSpeed = 0)
    {
        float totalSpeed = (Speed.Value + extraSpeed) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, totalSpeed);
        _animation.RUNNING(_movementDirection, finishFunction);
    }
    /// <summary>
    /// Движение по направлению
    /// </summary>
    /// <param name="direction">Направление</param>
    protected void Move(Vector2 direction)
    {
        _rigBody.velocity = direction;
    }
    /// <summary>
    /// Сброс движения
    /// </summary>
    protected void ResetVelocity() => Move(Vector2.zero);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + HitboxOffset, new TileHalf(HitboxDistance));
    }
}