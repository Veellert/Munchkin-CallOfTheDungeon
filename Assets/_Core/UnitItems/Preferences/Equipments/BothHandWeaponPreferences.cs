using UnityEngine;

[CreateAssetMenu(fileName = "NewBothHandWeapon", menuName = "Items/Equipments/Create Both Hand Weapon", order = 1)]
public class BothHandWeaponPreferences : WeaponPreferences
{
    protected override eEquipmentType GetEquipmentType()
        => eEquipmentType.BothHandWeapon;
}
