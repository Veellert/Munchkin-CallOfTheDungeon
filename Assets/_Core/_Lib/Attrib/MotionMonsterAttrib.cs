using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMotionMonsterAttrib : IBaseUnitAttrib
{
    public UnitAttrib IdleMovmentRadius { get; set; }
    public UnitAttrib ChaseRadius { get; set; }
    public UnitAttrib StayPointTime { get; set; }
    public UnitAttrib RemovePointTime { get; set; }
    public int Level { get; set; }
    public bool IsBoss { get; set; }
}

public class MotionMonsterAttrib : BaseUnitAttrib, IMotionMonsterAttrib
{
    [SerializeField] private UnitAttrib _idleMovmentRadius = new UnitAttrib(2, 10);
    [SerializeField] private UnitAttrib _chaseRadius = new UnitAttrib(4, 10);
    [SerializeField] private UnitAttrib _stayPointTime = new UnitAttrib(2, 5);
    [SerializeField] private UnitAttrib _removePointTime = 5;
    [SerializeField] private int _level = 1;
    [SerializeField] private bool _isBoss = false;

    public UnitAttrib IdleMovmentRadius { get => _idleMovmentRadius; set => _idleMovmentRadius = value; }
    public UnitAttrib ChaseRadius { get => _chaseRadius; set => _chaseRadius = value; }
    public UnitAttrib StayPointTime { get => _stayPointTime; set => _stayPointTime = value; }
    public UnitAttrib RemovePointTime { get => _removePointTime; set => _removePointTime = value; }
    public int Level { get => _level; set => _level = value; }
    public bool IsBoss { get => _isBoss; set => _isBoss = value; }
}
