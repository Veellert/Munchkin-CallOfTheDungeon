using UnityEngine;

[CreateAssetMenu(fileName = "NewBoots", menuName = "Items/Equipments/Create Boots", order = 3)]
public class BootsPreferences : ArmorPreferences
{
    [SerializeField] protected float _speed = 1;
    public float Speed => _speed;

    protected override eEquipmentType GetEquipmentType()
        => eEquipmentType.Boots;
}
