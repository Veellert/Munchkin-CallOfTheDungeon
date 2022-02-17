using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки временного зелья
/// </summary>
[CreateAssetMenu(fileName = "NewTemporaryPotion", menuName = "Items/Potions/Create Temporary Potion")]
public class TemporaryPotionPreferences : PotionPreferences
{
    [SerializeField] private float _effectDuration = 5;
    public float EffectDuration => _effectDuration;
}