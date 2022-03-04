using UnityEngine;

/// <summary>
/// ������ ������������ �����
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
    /// ������������� ������
    /// </summary>
    /// <param name="item">�������</param>
    public void Initialize(InventoryCell item)
    {
        transform.position = item.transform.position;
        _item.Initialize(item.GetCurrentItem());
    }

    /// <summary>
    /// ������������� ���������
    /// </summary>
    public void InverseActive() => gameObject.SetActive(!gameObject.activeSelf);
}
