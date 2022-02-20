using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки подвижного в спокойном состоянии монстра
/// </summary>
[CreateAssetMenu(fileName = "NewMonster", menuName = "Units/Create IdleMovable Monster", order = 4)]
public class IdleMovableMonsterPreferences : AgressiveMonsterPreferences, IIdleMovableAttrib
{
    [Header("Idle Movement Attributes")]
    [SerializeField] [Min(0.1f)] protected float _idleMovementRadius = 1;
    public float IdleMovementRadius => _idleMovementRadius;

    [SerializeField] [Min(0.1f)] protected float _stayPointTime = 2;
    public float StayPointTime => _stayPointTime;
    [SerializeField] [Min(5)] protected float _removePointTime = 5;
    public float RemovePointTime => _removePointTime;
}