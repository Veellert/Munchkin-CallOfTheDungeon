using UnityEngine;

[CreateAssetMenu(fileName = "NewOneHandWeapon", menuName = "Items/Equipments/Create One Hand Weapon", order = 0)]
public class OneHandWeaponPreferences : WeaponPreferences
{
    protected override eEquipmentType GetEquipmentType()
        => eEquipmentType.OneHandWeapon;
}
