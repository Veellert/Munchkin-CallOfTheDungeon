using UnityEngine;

public abstract class ArmorPreferences : EquipmentPreferences
{
    [SerializeField] [Min(1)] protected float _addictiveHP = 1;
    public float AddictiveHP => _addictiveHP;
}
