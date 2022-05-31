using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���������
/// </summary>
public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private InventoryCell _itemCell;
    [SerializeField] private InventoryCellMouseFollower _itemFollow;

    [SerializeField] private RectTransform _activeItemContent;
    [SerializeField] private RectTransform _equipmentItemContent;
    [SerializeField] private RectTransform _currentEquipmentContent;

    [SerializeField] private InventoryItemDescription _itemDescription;

    private List<InventoryCell> _inventoryItemList = new List<InventoryCell>();
    private List<InventoryCell> _inventoryEquipmentList = new List<InventoryCell>();
    private List<InventoryCell> _currentEquipmentList = new List<InventoryCell>();

    private InventoryCell _selectedItem;

    private void Start()
    {
        Player.Instance.Inventory.OnItemAdded += OnItemAddedToPlayer;
        Player.Instance.EquipmentInventory.OnEquipmentAdded += OnEquipmentAddedToPlayer;
        Player.Instance.EquipmentInventory.OnEquipmentUsed += OnEquipmentUsedOnPlayer;
    }

    private void OnEnable()
    {
        if (Player.Instance.Inventory.GetInventoryItems().Count != _inventoryItemList.Count)
        {
            _inventoryItemList.Clear();
            foreach (var item in Player.Instance.Inventory.GetInventoryItems())
                _inventoryItemList.Add(CreateInventoryItem(item));
        }
        if (Player.Instance.EquipmentInventory.GetInventoryEquipments().Count != _inventoryEquipmentList.Count)
        {
            _inventoryEquipmentList.Clear();
            foreach (var equipment in Player.Instance.EquipmentInventory.GetInventoryEquipments())
                _inventoryEquipmentList.Add(CreateInventoryEquipment(equipment));
        }
    }

    private void OnDisable()
    {
        _itemFollow.gameObject.SetActive(false);
        _selectedItem?.SetSelection(false);
    }

    /// <summary>
    /// ��� ���������� �������� � ���������
    /// </summary>
    /// <param name="item">�������</param>
    private void OnItemAddedToPlayer(BaseItem item)
    {
        _inventoryItemList.Add(CreateInventoryItem(item));
    }
    /// <summary>
    /// ��� ���������� ���������� � ���������
    /// </summary>
    /// <param name="equipment">����������</param>
    private void OnEquipmentAddedToPlayer(BaseEquipment equipment)
    {
        _inventoryEquipmentList.Add(CreateInventoryEquipment(equipment));
    }
    /// <summary>
    /// ��� ������������� ����������
    /// </summary>
    /// <param name="equipment">����������</param>
    private void OnEquipmentUsedOnPlayer(BaseEquipment equipment)
    {
        _currentEquipmentList.Add(CreateCurrentEquipment(equipment));
    }

    /// <summary>
    /// �������� ������ � ������� ����������
    /// </summary>
    /// <param name="equipment">������ � ������� �����������</param>
    private void RemoveCurrentEquipment(InventoryCell equipment)
    {
        _currentEquipmentList.Remove(equipment);
        Player.Instance.EquipmentInventory.RemoveCurrentEquipmentFromInventory((BaseEquipment)equipment.GetCurrentItem());

        Destroy(equipment.gameObject);
    }
    /// <summary>
    /// �������� ������ � ���������� �� ���������
    /// </summary>
    /// <param name="equipment">������ � �����������</param>
    private void RemoveEquipmentFromInventory(InventoryCell equipment)
    {
        _inventoryEquipmentList.Remove(equipment);
        Player.Instance.EquipmentInventory.RemoveEquipmentFromInventory((BaseEquipment)equipment.GetCurrentItem());

        Destroy(equipment.gameObject);
    }
    /// <summary>
    /// �������� ������ � �������� �� ���������
    /// </summary>
    /// <param name="item">������ � ���������</param>
    private void RemoveItemFromInventory(InventoryCell item)
    {
        _inventoryItemList.Remove(item);
        Player.Instance.Inventory.RemoveItemFromInventory(item.GetCurrentItem());

        Destroy(item.gameObject);
    }

    /// <returns>
    /// ��������� ������ � ������� �����������
    /// </returns>
    /// <param name="equipment">����������</param>
    private InventoryCell CreateCurrentEquipment(BaseEquipment equipment)
    {
        var currentEquipment = Instantiate(_itemCell, Vector2.zero, Quaternion.identity);
        currentEquipment.transform.SetParent(_currentEquipmentContent);

        currentEquipment.Initialize(equipment);

        currentEquipment.OnItemSelect += OnCurrentEquipmentSelect;
        currentEquipment.OnItemUse += OnCurrentEquipmentUse;

        return currentEquipment;
    }
    /// <returns>
    /// ��������� ������ � �����������
    /// </returns>
    /// <param name="equipment">����������</param>
    private InventoryCell CreateInventoryEquipment(BaseEquipment equipment)
    {
        var inventoryEquipment = Instantiate(_itemCell, Vector2.zero, Quaternion.identity);
        inventoryEquipment.transform.SetParent(_equipmentItemContent);

        inventoryEquipment.Initialize(equipment);

        inventoryEquipment.OnItemSelect += OnEquipmentSelect;
        inventoryEquipment.OnItemUse += OnEquipmentUse;
        inventoryEquipment.OnItemDrop += OnEquipmentDrop;
        inventoryEquipment.OnItemBeginDrag += OnItemBeginDrag;
        inventoryEquipment.OnItemEndDrag += OnItemEndDrag;

        return inventoryEquipment;
    }
    /// <returns>
    /// ��������� ������ � ���������
    /// </returns>
    /// <param name="item">�������</param>
    private InventoryCell CreateInventoryItem(BaseItem item)
    {
        var inventoryItem = Instantiate(_itemCell, Vector2.zero, Quaternion.identity);
        inventoryItem.transform.SetParent(_activeItemContent);

        inventoryItem.Initialize(item);

        inventoryItem.OnItemSelect += OnItemSelect;
        inventoryItem.OnItemUse += OnItemUse;
        inventoryItem.OnItemDrop += OnItemDrop;
        inventoryItem.OnItemBeginDrag += OnItemBeginDrag;
        inventoryItem.OnItemEndDrag += OnItemEndDrag;

        return inventoryItem;
    }

    /// <summary>
    /// ��� ������ ������ ������� ����������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnCurrentEquipmentSelect(InventoryCell inventoryItem)
    {
        _selectedItem?.SetSelection(false);
        _selectedItem = inventoryItem;

        _selectedItem.SetSelection(true);
        _itemDescription.InitializeData(_selectedItem, Player.Instance.EquipmentInventory.GetCurrentInventoryEquipments());
    }
    /// <summary>
    /// ��� ������ ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnEquipmentSelect(InventoryCell inventoryItem)
    {
        _selectedItem?.SetSelection(false);
        _selectedItem = inventoryItem;

        _selectedItem.SetSelection(true);
        _itemDescription.InitializeData(_selectedItem, Player.Instance.EquipmentInventory.GetInventoryEquipments());
    }
    /// <summary>
    /// ��� ������ ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnItemSelect(InventoryCell inventoryItem)
    {
        _selectedItem?.SetSelection(false);
        _selectedItem = inventoryItem;

        _selectedItem.SetSelection(true);
        _itemDescription.InitializeData(_selectedItem, Player.Instance.Inventory.GetInventoryItems());
    }

    /// <summary>
    /// ��� ������������� ������ ������� ����������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnCurrentEquipmentUse(InventoryCell inventoryItem)
    {
        inventoryItem.GetCurrentItem().UseItem();
        Player.Instance.EquipmentInventory.AddEquipmentToInventory((BaseEquipment)inventoryItem.GetCurrentItem());
        RemoveCurrentEquipment(inventoryItem);
    }
    /// <summary>
    /// ��� ������������� ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnEquipmentUse(InventoryCell inventoryItem)
    {
        if (Player.Instance.EquipmentInventory.GetCurrentInventoryEquipments().Exists(s =>
         ((EquipmentPreferences)s.GetPreferences()).Type == ((EquipmentPreferences)inventoryItem.GetCurrentItem().GetPreferences()).Type))
            return;

        inventoryItem.GetCurrentItem().UseItem();
        Player.Instance.EquipmentInventory.AddCurrentEquipmentToInventory((BaseEquipment)inventoryItem.GetCurrentItem());
        RemoveEquipmentFromInventory(inventoryItem);
    }
    /// <summary>
    /// ��� ������������� ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnItemUse(InventoryCell inventoryItem)
    {
        inventoryItem.GetCurrentItem().UseItem();
        RemoveItemFromInventory(inventoryItem);
    }

    /// <summary>
    /// �������� ������� ������
    /// </summary>
    /// <param name="selectedItem">��������� ������</param>
    /// <param name="targetItem">������� ������</param>
    private void SwapItems(InventoryCell selectedItem, InventoryCell targetItem, ref List<InventoryCell> content)
    {
        var currentIndex = content.IndexOf(selectedItem);
        var dropIndex = content.IndexOf(targetItem);

        var currentItem = selectedItem.GetCurrentItem();
        var dropItem = targetItem.GetCurrentItem();

        content[currentIndex].Initialize(dropItem);
        content[dropIndex].Initialize(currentItem);
    }
    /// <summary>
    /// ��� ���������� ������ � ���������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnItemDrop(InventoryCell inventoryItem)
    {
        SwapItems(_selectedItem, inventoryItem, ref _inventoryItemList);
        OnItemSelect(inventoryItem);
    }
    /// <summary>
    /// ��� ���������� ������ � �����������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnEquipmentDrop(InventoryCell inventoryItem)
    {
        SwapItems(_selectedItem, inventoryItem, ref _inventoryEquipmentList);
        OnEquipmentSelect(inventoryItem);
    }

    /// <summary>
    /// � ������ �������������� ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnItemBeginDrag(InventoryCell inventoryItem)
    {
        OnItemSelect(inventoryItem);

        _itemFollow.InverseActive();
        _itemFollow.Initialize(inventoryItem);
    }
    /// <summary>
    /// � ����� �������������� ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnItemEndDrag(InventoryCell inventoryItem)
    {
        _itemFollow.InverseActive();
    }
}
