using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ������ ���������
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
    /// ������� ��� ������ ��������
    /// </summary>
    public event Action<InventoryCell> OnItemSelect;
    /// <summary>
    /// ������� ��� ������������� ��������
    /// </summary>
    public event Action<InventoryCell> OnItemUse;

    /// <summary>
    /// ������� ��� ���������� ��������
    /// </summary>
    public event Action<InventoryCell> OnItemDrop;
    /// <summary>
    /// ������� ��� ������ �������������� ��������
    /// </summary>
    public event Action<InventoryCell> OnItemBeginDrag;
    /// <summary>
    /// ������� ��� ����� �������������� ��������
    /// </summary>
    public event Action<InventoryCell> OnItemEndDrag;

    /// <returns>
    /// ������� �������
    /// </returns>
    public BaseItem GetCurrentItem() => _item;

    /// <summary>
    /// ������������� ������ ���������
    /// </summary>
    /// <param name="item">�������</param>
    public void Initialize(BaseItem item)
    {
        _item = item;

        _image.sprite = ItemPreferences.Sprite;
    }

    /// <summary>
    /// ������� ������
    /// </summary>
    /// <param name="isSelected">�������������</param>
    public void SetSelection(bool isSelected)
    {
        if(_cellImage)
            _cellImage.sprite = isSelected
                ? _selectedCellSprite
                : _cellSprite;
    }

    /// <summary>
    /// ��� ������� �� ������ � ������� �����
    /// </summary>
    /// <param name="eventData">������ � �������</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OnItemUse?.Invoke(this);
        else if (eventData.button == PointerEventData.InputButton.Left)
            OnItemSelect?.Invoke(this);
    }

    /// <summary>
    /// � ������ �������������� � ������� �����
    /// </summary>
    /// <param name="eventData">������ � �������</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnItemBeginDrag?.Invoke(this);
    }
    /// <summary>
    /// �� ����� �������������� � ������� �����
    /// </summary>
    /// <param name="eventData">������ � �������</param>
    public void OnDrag(PointerEventData eventData)
    {

    }
    /// <summary>
    /// ��� ���������� � ������� �����
    /// </summary>
    /// <param name="eventData">������ � �������</param>
    public void OnDrop(PointerEventData eventData)
    {
        OnItemDrop?.Invoke(this);
    }
    /// <summary>
    /// � ����� �������������� � ������� �����
    /// </summary>
    /// <param name="eventData">������ � �������</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }
}
