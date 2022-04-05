using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� �������� � ���� ��������
/// </summary>
public class TraderItemDescription : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private Text _itemName;
    [SerializeField] private Text _itemDescription;
    [SerializeField] private Text _itemCost;

    private void OnEnable()
    {
        VisualizeData(false);
    }

    /// <summary>
    /// ������������� ��������
    /// </summary>
    /// <param name="item">��������� ������</param>
    public void InitializeData(InventoryCell item, CostItem costItem)
    {
        VisualizeData();

        _image.sprite = item.ItemPreferences.Sprite;

        _itemName.text = item.ItemPreferences.Name;
        _itemDescription.text = item.ItemPreferences.Description;

        _itemCost.text = "���������: " + costItem.Cost + " ����������";
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
        _itemCost.gameObject.SetActive(toggle);
    }
}
