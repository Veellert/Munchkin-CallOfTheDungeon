using System;
using UnityEngine;

[Serializable]
public class CostItem
{
    [SerializeField] private GameObject _item;
    public GameObject Item { get => _item; }
    [SerializeField] private int _cost;
    public int Cost { get => _cost; }
}