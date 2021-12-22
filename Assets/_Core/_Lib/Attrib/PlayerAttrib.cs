using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAttrib : IBaseUnitAttrib
{
    public UnitAttrib DodgeForce { get; set; }
    public UnitAttrib DodgeCooldown { get; set; }
}

public class PlayerAttrib : BaseUnitAttrib, IPlayerAttrib
{
    [SerializeField] private UnitAttrib _dodgeForce = new UnitAttrib(6, 10);
    [SerializeField] private UnitAttrib _dodgeCooldown = new UnitAttrib(2, 5);

    public UnitAttrib DodgeForce { get => _dodgeForce; set => _dodgeForce = value; }
    public UnitAttrib DodgeCooldown { get => _dodgeCooldown; set => _dodgeCooldown = value; }
}

