using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за инвентарь экипировки игрока
/// </summary>
public class PlayerEquipmentInventory : MonoBehaviour
{
    /// <summary>
    /// Событие по добавлению экипировки в инвентарь
    /// </summary>
    public event Action<BaseEquipment> OnEquipmentAdded;
    
    /// <summary>
    /// Событие при использовании экипировки
    /// </summary>
    public event Action<BaseEquipment> OnEquipmentUsed;

    //===>> Components & Fields <<===\\

    private List<BaseEquipment> _equipmentList = new List<BaseEquipment>();
    private List<BaseEquipment> _currentEquipmentList = new List<BaseEquipment>();

    //===>> Public Methods <<===\\ 

    /// <summary>
    /// Добавить экипировку в инвентарь
    /// </summary>
    /// <param name="equipment">Экипировка</param>
    public void AddEquipmentToInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _equipmentList.Add(equipment);
        OnEquipmentAdded?.Invoke(equipment);
    }

    /// <summary>
    /// Удалить экипировку из инвентаря
    /// </summary>
    /// <param name="equipment">Экипировка</param>
    public void RemoveEquipmentFromInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _equipmentList.RemoveAt(_equipmentList.IndexOf(equipment));
    }

    /// <returns>
    /// Вся экипировка из инвентаря
    /// </returns>
    public List<BaseEquipment> GetInventoryEquipments() => _equipmentList;

    /// <summary>
    /// Надеть экипировку
    /// </summary>
    /// <param name="equipment">Экипировка</param>
    public void AddCurrentEquipmentToInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _currentEquipmentList.Add(equipment);
        OnEquipmentUsed?.Invoke(equipment);
    }

    /// <summary>
    /// Удалить надетую экипировку
    /// </summary>
    /// <param name="equipment">Экипировка</param>
    public void RemoveCurrentEquipmentFromInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _currentEquipmentList.RemoveAt(_currentEquipmentList.IndexOf(equipment));
    }

    /// <returns>
    /// Вся надетая экипировка
    /// </returns>
    public List<BaseEquipment> GetCurrentInventoryEquipments() => _currentEquipmentList;
}
