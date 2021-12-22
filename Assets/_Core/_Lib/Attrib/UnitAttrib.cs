using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitAttrib
{
    [SerializeField] private float _value;
    public float Value { get => _value; private set => _value = value; }

    [SerializeField] private float _maxValue;
    public float MaxValue { get => _maxValue; private set => _maxValue = value; }

    [SerializeField] private float _originalValue;
    public float OriginalValue { get => _originalValue; private set => _originalValue = value; }

    public UnitAttrib(float value)
    {
        Value = MaxValue = OriginalValue = value;
    }
    
    public UnitAttrib(float value, float maxValue)
    {
        Value = value;
        MaxValue = OriginalValue = maxValue;
    }

    public float IncreaseValue(float value = 1)
    {
        return Value = Value + value > MaxValue ? FillToMax() : Value + value;
    }

    public float DecreaseValue(float value = 1)
    {
        return Value = Value - value < 0 ? 0 : Value - value;
    }

    public float FillToMax()
    {
        return Value = MaxValue;
    }
    
    public float FillEmpty()
    {
        return Value = 0;
    }

    public float SetMax(float maxValue)
    {
        return MaxValue = maxValue;
    }

    public float ResetAttribute(float originalValue = 0)
    {
        if (originalValue != 0)
            OriginalValue = originalValue;

        MaxValue = OriginalValue;
        FillToMax();

        return Value;
    }

    public bool IsValueEmpty() => Value <= 0;

    #region Operators

    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return Value;
                case 1: return MaxValue;
                case 2: return OriginalValue;
                default: Debug.LogException(new IndexOutOfRangeException("ћаксимальное значение индекса равн€етс€ двум!")); return -1;
            }
        }
        set
        {
            switch (index)
            {
                case 0: Value = value; break;
                case 1: MaxValue = value; break;
                case 2: OriginalValue = value; break;
                default: Debug.LogException(new IndexOutOfRangeException("ћаксимальное значение индекса равн€етс€ двум!")); break;
            }
        }
    }

    public static UnitAttrib operator !(UnitAttrib atr)
    {
        atr.Value = atr.Value >= atr.MaxValue / 2 ? 0 : atr.MaxValue;

        return atr;
    }

    public static UnitAttrib operator --(UnitAttrib atr)
    {
        atr.DecreaseValue();
        return atr;
    }
    public static UnitAttrib operator -(UnitAttrib atr1, UnitAttrib atr2)
    {
        atr1.DecreaseValue(atr2.Value);
        return atr1;
    }

    public static UnitAttrib operator ++(UnitAttrib atr)
    {
        atr.IncreaseValue();
        return atr;
    }
    public static UnitAttrib operator +(UnitAttrib atr1, UnitAttrib atr2)
    {
        atr1.IncreaseValue(atr2.Value);
        return atr1;
    }

    public static implicit operator UnitAttrib(float v) => new UnitAttrib(v);
    public static implicit operator float(UnitAttrib v) => v.Value;

    public static implicit operator string(UnitAttrib v) => v.ToString();
    public override string ToString() => $"{Value} / {MaxValue}";

    #endregion
}
