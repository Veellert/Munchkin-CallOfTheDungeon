using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ���������� �� ��������� ���������� ������
/// </summary>
public class PlayerEquipmentInventory : MonoBehaviour
{
    /// <summary>
    /// ������� �� ���������� ���������� � ���������
    /// </summary>
    public event Action<BaseEquipment> OnEquipmentAdded;
    
    /// <summary>
    /// ������� ��� ������������� ����������
    /// </summary>
    public event Action<BaseEquipment> OnEquipmentUsed;

    //===>> Components & Fields <<===\\

    private List<BaseEquipment> _equipmentList = new List<BaseEquipment>();
    private List<BaseEquipment> _currentEquipmentList = new List<BaseEquipment>();

    //===>> Public Methods <<===\\ 

    /// <summary>
    /// �������� ���������� � ���������
    /// </summary>
    /// <param name="equipment">����������</param>
    public void AddEquipmentToInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _equipmentList.Add(equipment);
        OnEquipmentAdded?.Invoke(equipment);
    }

    /// <summary>
    /// ������� ���������� �� ���������
    /// </summary>
    /// <param name="equipment">����������</param>
    public void RemoveEquipmentFromInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _equipmentList.RemoveAt(_equipmentList.IndexOf(equipment));
    }

    /// <returns>
    /// ��� ���������� �� ���������
    /// </returns>
    public List<BaseEquipment> GetInventoryEquipments() => _equipmentList;

    /// <summary>
    /// ������ ����������
    /// </summary>
    /// <param name="equipment">����������</param>
    public void AddCurrentEquipmentToInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _currentEquipmentList.Add(equipment);
        OnEquipmentUsed?.Invoke(equipment);
    }

    /// <summary>
    /// ������� ������� ����������
    /// </summary>
    /// <param name="equipment">����������</param>
    public void RemoveCurrentEquipmentFromInventory(BaseEquipment equipment)
    {
        if (!equipment)
            return;

        _currentEquipmentList.RemoveAt(_currentEquipmentList.IndexOf(equipment));
    }

    /// <returns>
    /// ��� ������� ����������
    /// </returns>
    public List<BaseEquipment> GetCurrentInventoryEquipments() => _currentEquipmentList;
}
