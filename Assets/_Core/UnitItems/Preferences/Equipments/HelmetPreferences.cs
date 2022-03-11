using UnityEngine;

[CreateAssetMenu(fileName = "NewHelmet", menuName = "Items/Equipments/Create Helmet", order = 2)]
public class HelmetPreferences : ArmorPreferences
{
    protected override eEquipmentType GetEquipmentType()
        => eEquipmentType.Helmet;
}
