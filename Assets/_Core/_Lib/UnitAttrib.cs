using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ласс используемый дл€ аттрибутов у персонажей и не только
/// </summary>
/// <remarks>
/// ’ранит в себе текущее, максимальное и изначальное числовое значение
/// </remarks>
[Serializable]
public class UnitAttrib
{
    [SerializeField] private float _value;
    public float Value { get => _value; private set => _value = value; }
    [SerializeField] private float _maxValue;
    public float MaxValue { get => _maxValue; private set => _maxValue = value; }

    public float OriginalValue { get ; private set; }
    public float OriginalMaxValue { get ; private set; }

    [HideInInspector] public event EventHandler OnValueChanged;

    public UnitAttrib(float value)
    {
        Value = MaxValue = OriginalValue = OriginalMaxValue = value;
    }

    public UnitAttrib(float value, float maxValue)
    {
        Value = OriginalValue = value;
        MaxValue = OriginalMaxValue = maxValue;
    }

    /// <summary>
    /// ”величивает значение на значение параметра
    /// </summary>
    /// <param name="value">„исло на которое увеличить</param>
    public void IncreaseValue(float value = 1)
    {
        Value = Value + value > MaxValue ? MaxValue : Value + value;

        OnValueChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ”меньшает значение на значение параметра
    /// </summary>
    /// <param name="value">„исло на которое уменьшить</param>
    public void DecreaseValue(float value = 1)
    {
        Value = Value - value < 0 ? 0 : Value - value;

        OnValueChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// «аполн€ет текущее значение до максимального
    /// </summary>
    public void FillToMax()
    {
        Value = MaxValue;

        OnValueChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ќчищает текущее значение
    /// </summary>
    public void FillEmpty()
    {
        Value = 0;

        OnValueChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Ќазначает макимальное значение на значение параметра
    /// </summary>
    /// <param name="maxValue">«начение на которое надо изменить</param>
    public void SetMax(float maxValue)
    {
        MaxValue = maxValue;
    }

    /// <summary>
    /// —брасывает текущие значени€ до изначальных
    /// </summary>
    public void ResetAttribute()
    {
        MaxValue = OriginalMaxValue;
        Value = OriginalValue;

        OnValueChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ѕровер€ет пустое ли текущее значение
    /// </summary>
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
