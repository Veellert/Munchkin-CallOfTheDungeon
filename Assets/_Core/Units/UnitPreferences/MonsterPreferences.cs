using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки монстра
/// </summary>
[CreateAssetMenu(fileName = "NewMonster", menuName = "Units/Create Monster")]
public class MonsterPreferences : UnitPreferences
{
    [SerializeField] private int _level = 1;
    public int Level => _level;
}