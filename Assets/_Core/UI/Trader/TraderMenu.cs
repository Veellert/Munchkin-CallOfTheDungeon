using System.Collections.Generic;
using UnityEngine;

public class TraderMenu : MonoBehaviour
{
    [SerializeField] private InventoryCell _itemCell;

    [SerializeField] private RectTransform _itemContent;

    [SerializeField] private TraderItemDescription _itemDescription;

    private List<(InventoryCell cell, CostItem costItem)> _itemList = new List<(InventoryCell cell, CostItem costItem)>();

    private (InventoryCell cell, CostItem costItem) _selectedItem;

    private void OnEnable()
    {
        if (!BaseTrader.ActiveTrader)
            return;

        foreach (var item in _itemList)
            Destroy(item.cell.gameObject);

        _itemList.Clear();
        foreach (var item in BaseTrader.ActiveTrader.GetItemList())
            _itemList.Add(CreateTradeItem(item));
    }

    private void OnDisable()
    {
        _selectedItem.cell?.SetSelection(false);
    }

    /// <returns>
    /// Созданная ячейка с предметом
    /// </returns>
    /// <param name="item">Предмет</param>
    private (InventoryCell cell, CostItem costItem) CreateTradeItem(CostItem item)
    {
        var inventoryItem = Instantiate(_itemCell, Vector2.zero, Quaternion.identity);
        inventoryItem.transform.SetParent(_itemContent);

        inventoryItem.Initialize(item.Item.GetComponent<BaseItem>());

        inventoryItem.OnItemSelect += OnItemSelect;
        inventoryItem.OnItemUse += OnItemUse;

        return (inventoryItem, item);
    }

    /// <summary>
    /// При выборе ячейки
    /// </summary>
    /// <param name="inventoryItem">Ячейка</param>
    private void OnItemSelect(InventoryCell inventoryItem)
    {
        _selectedItem.cell?.SetSelection(false);
        _selectedItem.cell = inventoryItem;
        _selectedItem.costItem = BaseTrader.ActiveTrader.GetItemList().
            Find(s => s.Item.GetComponent<BaseItem>().GetPreferences().ID == inventoryItem.ItemPreferences.ID);

        _selectedItem.cell.SetSelection(true);
        _itemDescription.InitializeData(_selectedItem.cell, _selectedItem.costItem);
    }

    /// <summary>
    /// При использовании ячейки
    /// </summary>
    /// <param name="inventoryItem">Ячейка</param>
    private void OnItemUse(InventoryCell inventoryItem)
    {
        var item = BaseTrader.ActiveTrader.GetItemList().
            Find(s => s.Item.GetComponent<BaseItem>().GetPreferences().ID == inventoryItem.ItemPreferences.ID);

        if (Player.Instance.Inventory.DropCrystals <= item.Cost)
            return;

        Player.Instance.Inventory.RemoveDropCrystals(item.Cost);
        Instantiate(item.Item, Player.Instance.transform.position, Quaternion.identity);
    }
}
