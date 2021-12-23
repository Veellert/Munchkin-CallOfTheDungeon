using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseUnitAttrib
{
    public string Name { get; set; }
    public UnitAttrib HP { get; set; }
    public UnitAttrib Speed { get; set; }
    public UnitAttrib Attack { get; set; }
    public UnitAttrib AttackCooldown { get; set; }
}

public class BaseUnitAttrib : MonoBehaviour, IBaseUnitAttrib
{
    [SerializeField] private UnitAttrib _hp = 100;
    [SerializeField] private UnitAttrib _speed = new UnitAttrib(5, 10);
    [SerializeField] private string _name = "ёнит без имени";
    [SerializeField] private UnitAttrib _attack = new UnitAttrib(25, 100);
    [SerializeField] private UnitAttrib _attackCooldown = 0.5f;

    public string Name { get => _name; set => _name = value; }
    public UnitAttrib HP { get => _hp; set => _hp = value; }
    public UnitAttrib Speed { get => _speed; set => _speed = value; }
    public UnitAttrib Attack { get => _attack; set => _attack = value; }
    public UnitAttrib AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }
}
