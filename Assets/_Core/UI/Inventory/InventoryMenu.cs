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
    [SerializeField] private InventoryItemDescription _itemDescription;

    private List<InventoryCell> _inventoryItemList = new List<InventoryCell>();
    private InventoryCell _selectedItem;

    private void Start()
    {
        Player.Instance.Inventory.OnItemAdded += OnItemAddedToPlayer;
    }

    private void OnEnable()
    {
        if (Player.Instance.Inventory.GetInventoryItems().Count == _inventoryItemList.Count)
            return;

        _inventoryItemList.Clear();
        foreach (var item in Player.Instance.Inventory.GetInventoryItems())
            _inventoryItemList.Add(CreateInventoryItem(item));
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
    /// ��� ������ ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnItemSelect(InventoryCell inventoryItem)
    {
        _selectedItem?.SetSelection(false);
        _selectedItem = inventoryItem;

        _selectedItem.SetSelection(true);
        _itemDescription.InitializeData(_selectedItem);
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
    private void SwapItems(InventoryCell selectedItem, InventoryCell targetItem)
    {
        var currentIndex = _inventoryItemList.IndexOf(selectedItem);
        var dropIndex = _inventoryItemList.IndexOf(targetItem);

        var currentItem = selectedItem.GetCurrentItem();
        var dropItem = targetItem.GetCurrentItem();

        _inventoryItemList[currentIndex].Initialize(dropItem);
        _inventoryItemList[dropIndex].Initialize(currentItem);
    }
    /// <summary>
    /// ��� ���������� ������
    /// </summary>
    /// <param name="inventoryItem">������</param>
    private void OnItemDrop(InventoryCell inventoryItem)
    {
        SwapItems(_selectedItem, inventoryItem);
        OnItemSelect(inventoryItem);
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
