using System;
using UnityEngine;

[Serializable]
public class DropItem
{
    [SerializeField] private GameObject _item;
    public GameObject Item { get => _item; }
    [SerializeField] private int _count;
    public int Count { get => _count; }
}
