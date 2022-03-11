using UnityEngine;

public abstract class WeaponPreferences : EquipmentPreferences
{
    [SerializeField] [Min(0.1f)] protected float _addictiveDamage = 1;
    public float AddictiveDamage => _addictiveDamage;
    
    [SerializeField] [Min(0.1f)] protected float _range = 1;
    public float Range => _range;
    
    [SerializeField] [Min(0.1f)] protected float _distance = 1;
    public float Distance => _distance;
    
    [SerializeField] [Min(0.1f)] protected float _cooldown = 1;
    public float Cooldown => _cooldown;
}
