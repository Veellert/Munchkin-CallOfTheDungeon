using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    UnitAttrib HP { get; set; }
    bool IsDead { get; }

    void Die();
    void GetDamage(float damageAmount);
    void Heal(float healAmount);
}

public interface IDamager
{
    UnitAttrib Damage { get; set; }
    UnitAttrib AttackCooldown { get; set; }

    void AttackInput();
    void AttackHandler();
    void CheckAttackCooldown();
}

public interface IIdleMovable
{
    UnitAttrib IdleMovmentRadius { get; set; }
    UnitAttrib RemovePointTime { get; set; }
    UnitAttrib StayPointTime { get; set; }

    void StayOnPoint();
    void RemovePoint();
    void InitPoint();
}

public interface IUnit
{
    string UnitName { get; set; }
    UnitAttrib Speed { get; set; }
}
