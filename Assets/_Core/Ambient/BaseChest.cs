using System.Collections.Generic;
using UnityEngine;

public class BaseChest : MonoBehaviour
{
    [SerializeField] private List<DropItem> _itemsList = new List<DropItem>();

    public List<DropItem> GetDrop() => _itemsList;
}
