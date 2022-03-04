using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Ячейка инвентаря
/// </summary>
public class InventoryCell : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
    IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _cellImage;

    [SerializeField] private Sprite _cellSprite;
    [SerializeField] private Sprite _selectedCellSprite;

    private BaseItem _item;

    public ItemPreferences ItemPreferences => _item.GetPreferences();

    /// <summary>
    /// Событие при выборе предмета
    /// </summary>
    public event Action<InventoryCell> OnItemSelect;
    /// <summary>
    /// Событие при использовании предмета
    /// </summary>
    public event Action<InventoryCell> OnItemUse;

    /// <summary>
    /// Событие при отпускания предмета
    /// </summary>
    public event Action<InventoryCell> OnItemDrop;
    /// <summary>
    /// Событие при начале перетаскивания предмета
    /// </summary>
    public event Action<InventoryCell> OnItemBeginDrag;
    /// <summary>
    /// Событие при конце перетаскивания предмета
    /// </summary>
    public event Action<InventoryCell> OnItemEndDrag;

    /// <returns>
    /// Текущий предмет
    /// </returns>
    public BaseItem GetCurrentItem() => _item;

    /// <summary>
    /// Инициализация ячейки инвентаря
    /// </summary>
    /// <param name="item">Предмет</param>
    public void Initialize(BaseItem item)
    {
        _item = item;

        _image.sprite = ItemPreferences.Sprite;
    }

    /// <summary>
    /// Выбрать ячейку
    /// </summary>
    /// <param name="isSelected">Переключатель</param>
    public void SetSelection(bool isSelected)
    {
        if(_cellImage)
            _cellImage.sprite = isSelected
                ? _selectedCellSprite
                : _cellSprite;
    }

    /// <summary>
    /// При нажатии на ячейку с помощью мышки
    /// </summary>
    /// <param name="eventData">Данные о нажатии</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OnItemUse?.Invoke(this);
        else if (eventData.button == PointerEventData.InputButton.Left)
            OnItemSelect?.Invoke(this);
    }

    /// <summary>
    /// В начале перетаскивания с помощью мышки
    /// </summary>
    /// <param name="eventData">Данные о нажатии</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnItemBeginDrag?.Invoke(this);
    }
    /// <summary>
    /// Во время перетаскивания с помощью мышки
    /// </summary>
    /// <param name="eventData">Данные о нажатии</param>
    public void OnDrag(PointerEventData eventData)
    {

    }
    /// <summary>
    /// При отпускании с помощью мышки
    /// </summary>
    /// <param name="eventData">Данные о нажатии</param>
    public void OnDrop(PointerEventData eventData)
    {
        OnItemDrop?.Invoke(this);
    }
    /// <summary>
    /// В конце перетаскивания с помощью мышки
    /// </summary>
    /// <param name="eventData">Данные о нажатии</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }
}
