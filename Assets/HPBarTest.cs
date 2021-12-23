using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarTest : MonoBehaviour
{
    private BaseUnitAttrib _unitAttrib;
    private UnitAttrib _timer = 3;

    private void Start()
    {
        _unitAttrib = GetComponent<MotionMonsterAttrib>();
    }

    private void FixedUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer.IsValueEmpty())
        {
            Damage();
            TryHeal();
        }
    }

    private void Damage()
    {
        _unitAttrib.HP -= 10;
        _timer.FillToMax();
    }

    private void TryHeal()
    {
        if (_unitAttrib.HP.IsValueEmpty())
            _unitAttrib.HP.FillToMax();
    }
}
