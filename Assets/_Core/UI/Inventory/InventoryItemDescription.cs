using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Описание предмета в инвентаре
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
    /// Инициализация описания
    /// </summary>
    /// <param name="item">Выбранная ячейка</param>
    public void InitializeData(InventoryCell item)
    {
        VisualizeData();

        _image.sprite = item.ItemPreferences.Sprite;

        _itemName.text = item.ItemPreferences.Name;
        _itemDescription.text = item.ItemPreferences.Description;

        int selectedItemCount = Player.Instance.Inventory.GetInventoryItems().
            FindAll(s => s.GetPreferences().ID == item.ItemPreferences.ID).Count;

        _inventoryInfo.text = "Всего в инвентаре: " + selectedItemCount + " шт.";
    }

    /// <summary>
    /// Установить видимость описания
    /// </summary>
    /// <param name="toggle">Переключатель</param>
    private void VisualizeData(bool toggle = true)
    {
        _image.gameObject.SetActive(toggle);

        _itemName.gameObject.SetActive(toggle);
        _itemDescription.gameObject.SetActive(toggle);
        _inventoryInfo.gameObject.SetActive(toggle);
    }
}
