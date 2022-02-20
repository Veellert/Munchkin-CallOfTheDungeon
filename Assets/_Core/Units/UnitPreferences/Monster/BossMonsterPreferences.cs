using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки босса
/// </summary>
[CreateAssetMenu(fileName = "NewBoss", menuName = "Units/Create Boss", order = 5)]
public class BossMonsterPreferences : AgressiveMonsterPreferences
{
    [Header("Boss Attributes")]
    [SerializeField] protected BossPhase _defaultBossPhase;
    public BossPhase DefaultBossPhase => _defaultBossPhase;
}
