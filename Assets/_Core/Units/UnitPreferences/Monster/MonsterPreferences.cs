using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки монстра
/// </summary>
[CreateAssetMenu(fileName = "NewMonster", menuName = "Units/Create Monster", order = 2)]
public class MonsterPreferences : UnitPreferences
{
    [Header("Basic Information")]
    [SerializeField] [Range(1, 20)] protected int _level = 1;
    public int Level => _level;

    [Header("Basic Attributes")]
    [SerializeField] [Min(0)] protected float _chaseRadius = 4;
    public float ChaseRadius => _chaseRadius;
}
