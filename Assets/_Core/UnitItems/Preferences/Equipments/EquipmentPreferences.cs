using System.Collections.Generic;
using UnityEngine;

public enum eEquipmentType
{
    OneHandWeapon = 0,
    BothHandWeapon = 1,
    Helmet = 2,
    Chestplate = 3,
    Boots = 4,
}

/// <summary>
/// Объект хранящий в себе настройки экипировки
/// </summary>
public abstract class EquipmentPreferences : ItemPreferences
{
    public eEquipmentType Type => GetEquipmentType();

    [SerializeField] private List<BaseEffectPreferences> _effectList = new List<BaseEffectPreferences>();
    public List<BaseEffectPreferences> EffectList => _effectList;

    protected abstract eEquipmentType GetEquipmentType();
}
