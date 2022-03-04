using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за инвентарь игрока
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Count of Dropped Crystals")]
    [SerializeField] private NumericAttrib _dropCrystals = new NumericAttrib(0, false);

    //===>> Attributes & Properties <<===\\

    public NumericAttrib DropCrystals { get => _dropCrystals; private set => _dropCrystals = value; }

    /// <summary>
    /// Событие по добавлению предмета в инвентарь
    /// </summary>
    public event Action<BaseItem> OnItemAdded;

    //===>> Components & Fields <<===\\

    private List<BaseItem> _itemList = new List<BaseItem>();

    //===>> Public Methods <<===\\

    /// <summary>
    /// Добавляет в инвентарь кристаллы
    /// </summary>
    /// <param name="crystalsCount">Кол-во кристаллов которые нужно добавить</param>
    public void AddDropCrystals(NumericAttrib crystalsCount)
    {
        DropCrystals += crystalsCount;
    }

    /// <summary>
    /// Добавить предмет в инвентарь
    /// </summary>
    /// <param name="item">Предмет</param>
    public void AddItemToInventory(BaseItem item)
    {
        if (!item)
            return;

        _itemList.Add(item);
        OnItemAdded?.Invoke(item);
    }
    
    /// <summary>
    /// Удалить предмет из инвентаря
    /// </summary>
    /// <param name="item">Предмет</param>
    public void RemoveItemFromInventory(BaseItem item)
    {
        if (!item)
            return;

        _itemList.RemoveAt(_itemList.IndexOf(item));
    }

    /// <returns>
    /// Все предметы из инвентаря
    /// </returns>
    public List<BaseItem> GetInventoryItems() => _itemList;
}