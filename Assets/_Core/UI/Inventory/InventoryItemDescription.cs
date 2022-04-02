using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� �������� � ���������
/// </summary>
public class InventoryItemDescription : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private Text _itemName;
    [SerializeField] private Text _itemDescription;
    [SerializeField] private Text _inventoryInfo;

    private void OnEnable()
    {
        VisualizeData(false);
    }

    /// <summary>
    /// ������������� ��������
    /// </summary>
    /// <param name="item">��������� ������</param>
    public void InitializeData(InventoryCell item, List<BaseItem> itemContainer)
    {
        VisualizeData();

        _image.sprite = item.ItemPreferences.Sprite;

        _itemName.text = item.ItemPreferences.Name;
        _itemDescription.text = item.ItemPreferences.Description;

        int selectedItemCount = itemContainer.FindAll(s => s.GetPreferences().ID == item.ItemPreferences.ID).Count;

        _inventoryInfo.text = "����� � ���������: " + selectedItemCount + " ��.";
    }
    /// <summary>
    /// ������������� ��������
    /// </summary>
    /// <param name="item">��������� ������</param>
    public void InitializeData(InventoryCell item, List<BaseEquipment> itemContainer)
    {
        VisualizeData();

        _image.sprite = item.ItemPreferences.Sprite;

        _itemName.text = item.ItemPreferences.Name;
        _itemDescription.text = item.ItemPreferences.Description;

        int selectedItemCount = itemContainer.FindAll(s => s.GetPreferences().ID == item.ItemPreferences.ID).Count;

        _inventoryInfo.text = "����� � ���������: " + selectedItemCount + " ��.";
    }

    /// <summary>
    /// ���������� ��������� ��������
    /// </summary>
    /// <param name="toggle">�������������</param>
    private void VisualizeData(bool toggle = true)
    {
        _image.gameObject.SetActive(toggle);

        _itemName.gameObject.SetActive(toggle);
        _itemDescription.gameObject.SetActive(toggle);
        _inventoryInfo.gameObject.SetActive(toggle);
    }
}
