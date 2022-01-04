using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��������
/// </summary>
public interface IDamageable
{
    UnitAttrib HP { get; set; }
    bool IsDead { get; }

    void Die();
    void GetDamage(float damageAmount);
    void Heal(float healAmount);
}

/// <summary>
/// ����� ���������
/// </summary>
public interface IDamager
{
    UnitAttrib Damage { get; set; }
    UnitAttrib AttackCooldown { get; set; }

    void AttackInput();
    void AttackHandler(float attackRange);
    void CheckAttackCooldown();
}

/// <summary>
/// ����� ������ � ��������� ��������� ������
/// </summary>
public interface IIdleMovable
{
    UnitAttrib IdleMovmentRadius { get; set; }
    UnitAttrib RemovePointTime { get; set; }
    UnitAttrib StayPointTime { get; set; }

    void StayOnPoint();
    void RemovePoint();
    void InitPoint();
}

/// <summary>
/// ���� �����
/// </summary>
public interface IBoss<T> where T : Enum
{
    T CurrentBossPhase { get; set; }

    void ChangeBossPhaseTo(T bossForm);
}

/// <summary>
/// �������� ������
/// </summary>
public interface IUnit
{
    string UnitName { get; set; }
    UnitAttrib Speed { get; set; }
}
