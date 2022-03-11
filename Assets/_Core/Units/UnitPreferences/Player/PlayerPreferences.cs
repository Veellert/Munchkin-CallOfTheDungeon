using UnityEngine;

/// <summary>
/// Гендер игрока
/// </summary>
public enum ePlayerGender
{
    Male = 0,
    Female = 1,
}

/// <summary>
/// Объект хранящий в себе настройки игрока
/// </summary>
[CreateAssetMenu(fileName = "NewPlayer", menuName = "Units/Create Player", order = 1)]
public class PlayerPreferences : UnitPreferences, IDamagerAttrib
{
    [Header("Basic Information")]
    [SerializeField] protected ePlayerGender _gender;
    public ePlayerGender Gender => _gender;

    [Header("Dodge Attributes")]
    [SerializeField] [Min(1)] protected float _dodgeForce = 5;
    public float DodgeForce => _dodgeForce;
    [SerializeField] [Min(0.1f)] protected float _dodgeCooldown = 1;
    public float DodgeCooldown => _dodgeCooldown;

    [Header("Attack Attributes")]
    [SerializeField] [Min(1)] protected float _damage = 5;
    public float Damage => _damage;
    [SerializeField] [Min(0.1f)] protected float _attackCooldown = 1;
    public float AttackCooldown => _attackCooldown;

    [SerializeField] [Min(0.1f)] protected float _attackRange = 0.5f;
    public float AttackRange => _attackRange;
    [SerializeField] [Min(0.1f)] protected float _attackDistance = 1;
    public float AttackDistance => _attackDistance;
}