using UnityEngine;

/// <summary>
/// Имеет здоровье
/// </summary>
public interface IDamageable
{
    NumericAttrib HP { get; }
    bool IsDead { get; }

    /// <summary>
    /// Смерть
    /// </summary>
    void Die();
    /// <summary>
    /// Получение уронв
    /// </summary>
    /// <param name="damageAmount">Кол-во урона</param>
    void GetDamage(float damageAmount);
    /// <summary>
    /// Восстановление здоровья
    /// </summary>
    /// <param name="healAmount">Кол-во здоровья для восполнения</param>
    void Heal(float healAmount);
}

/// <summary>
/// Может атаковать
/// </summary>
public interface IDamager
{
    NumericAttrib Damage { get; }
    NumericAttrib AttackRange { get; }
    NumericAttrib AttackCooldown { get; }
    bool CanAttack { get; }

    /// <summary>
    /// Начать атаку
    /// </summary>
    void AttackInput();
    /// <summary>
    /// Обработка атаки
    /// </summary>
    void AttackHandler();
}

/// <summary>
/// Может ходить в спокойном состоянии вокруг
/// </summary>
public interface IIdleMovable
{
    NumericAttrib IdleMovmentRadius { get; }
    NumericAttrib RemovePointTime { get; }
    NumericAttrib StayPointTime { get; }

    /// <summary>
    /// Остановиться на точке
    /// </summary>
    void StayOnPoint();
    /// <summary>
    /// Удалить точку
    /// </summary>
    void RemovePoint();
    /// <summary>
    /// Инициализировать точку
    /// </summary>
    void InitPoint();
}

/// <summary>
/// Является юнитом
/// </summary>
public interface IUnit
{
    string UnitName { get; }
    NumericAttrib Speed { get; }
    UnitStateMachine StateMachine { get; }
    float HitboxDistance { get; }
    Vector2 HitboxOffset { get; }
}
