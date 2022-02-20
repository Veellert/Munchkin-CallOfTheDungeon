using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки зелья
/// </summary>
[CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potions/Create Potion", order = 0)]
public class PotionPreferences : ItemPreferences
{
    [SerializeField] private float _maxUsageCount = 1;
    public float MaxUsageCount => _maxUsageCount;
}
