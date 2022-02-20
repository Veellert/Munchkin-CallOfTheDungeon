using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки агрессивного монстра
/// </summary>
[CreateAssetMenu(fileName = "NewMonster", menuName = "Units/Create Agressive Monster", order = 3)]
public class AgressiveMonsterPreferences : MonsterPreferences, IDamagerAttrib
{
    [Header("Attack Attributes")]
    [SerializeField] [Min(1)] protected float _damage = 5;
    public float Damage => _damage;
    [SerializeField] [Min(0.1f)] protected float _attackCooldown = 1;
    public float AttackCooldown => _attackCooldown;

    [SerializeField] [Min(0.1f)] protected float _attackRange = 0.5f;
    public float AttackRange => _attackRange;
}
