using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarTest : MonoBehaviour
{
    [SerializeField] private IDamageable _target;
    private UnitAttrib _timer = 3;

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
        _target.HP -= 10;
        _timer.FillToMax();
    }

    private void TryHeal()
    {
        if (_target.HP.IsValueEmpty())
            _target.HP.FillToMax();
    }
}
