using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseUnitAttrib
{
    public UnitAttrib HP { get; set; }
    public UnitAttrib Speed { get; set; }
    public string Name { get; set; }
}

public class BaseUnitAttrib : MonoBehaviour, IBaseUnitAttrib
{
    [SerializeField] private UnitAttrib _hp = 100;
    [SerializeField] private UnitAttrib _speed = new UnitAttrib(5, 10);
    [SerializeField] private string _name = "ёнит без имени";

    public UnitAttrib HP { get => _hp; set => _hp = value; }
    public UnitAttrib Speed { get => _speed; set => _speed = value; }
    public string Name { get => _name; set => _name = value; }
}
