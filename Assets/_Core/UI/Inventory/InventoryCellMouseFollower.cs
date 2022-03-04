using UnityEngine;

/// <summary>
/// Ячейка приследующая мышку
/// </summary>
public class InventoryCellMouseFollower : MonoBehaviour
{
    [SerializeField] private InventoryCell _item;

    private void Start()
    {
        InverseActive();
    }

    private void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Инициализация ячейки
    /// </summary>
    /// <param name="item">Предмет</param>
    public void Initialize(InventoryCell item)
    {
        transform.position = item.transform.position;
        _item.Initialize(item.GetCurrentItem());
    }

    /// <summary>
    /// Инвертировать видимость
    /// </summary>
    public void InverseActive() => gameObject.SetActive(!gameObject.activeSelf);
}
