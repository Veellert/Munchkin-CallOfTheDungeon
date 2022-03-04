using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Меню инвентаря
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
    /// При добавлении предмета в инвентарь
    /// </summary>
    /// <param name="item">Предмет</param>
    private void OnItemAddedToPlayer(BaseItem item)
    {
        _inventoryItemList.Add(CreateInventoryItem(item));
    }

    /// <summary>
    /// Удаление ячейки и предмета из инвентаря
    /// </summary>
    /// <param name="item">Ячейка с предметом</param>
    private void RemoveItemFromInventory(InventoryCell item)
    {
        _inventoryItemList.Remove(item);
        Player.Instance.Inventory.RemoveItemFromInventory(item.GetCurrentItem());

        Destroy(item.gameObject);
    }

    /// <returns>
    /// Созданная ячейка с предметом
    /// </returns>
    /// <param name="item">Предмет</param>
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
    /// При выборе ячейки
    /// </summary>
    /// <param name="inventoryItem">Ячейка</param>
    private void OnItemSelect(InventoryCell inventoryItem)
    {
        _selectedItem?.SetSelection(false);
        _selectedItem = inventoryItem;

        _selectedItem.SetSelection(true);
        _itemDescription.InitializeData(_selectedItem);
    }
    /// <summary>
    /// При использовании ячейки
    /// </summary>
    /// <param name="inventoryItem">Ячейка</param>
    private void OnItemUse(InventoryCell inventoryItem)
    {
        inventoryItem.GetCurrentItem().UseItem();
        RemoveItemFromInventory(inventoryItem);
    }

    /// <summary>
    /// Поменять местами ячейки
    /// </summary>
    /// <param name="selectedItem">Выбранная ячейка</param>
    /// <param name="targetItem">Целевая ячейка</param>
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
    /// При отпускании ячейки
    /// </summary>
    /// <param name="inventoryItem">Ячейка</param>
    private void OnItemDrop(InventoryCell inventoryItem)
    {
        SwapItems(_selectedItem, inventoryItem);
        OnItemSelect(inventoryItem);
    }

    /// <summary>
    /// В начале перетаскивания ячейки
    /// </summary>
    /// <param name="inventoryItem">Ячейка</param>
    private void OnItemBeginDrag(InventoryCell inventoryItem)
    {
        OnItemSelect(inventoryItem);

        _itemFollow.InverseActive();
        _itemFollow.Initialize(inventoryItem);
    }
    /// <summary>
    /// В конце перетаскивания ячейки
    /// </summary>
    /// <param name="inventoryItem">Ячейка</param>
    private void OnItemEndDrag(InventoryCell inventoryItem)
    {
        _itemFollow.InverseActive();
    }
}
