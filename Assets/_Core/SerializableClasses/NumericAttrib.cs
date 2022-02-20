using System;
using UnityEngine;

/// <summary>
/// Числовой атрибут
/// </summary>
/// <remarks>
/// Хранит в себе текущее, максимальное и изначальное числовое значение
/// </remarks>
[Serializable]
public class NumericAttrib
{
    [SerializeField] private bool _hasMaxValue = true;
    public bool HasMaxValue { get => _hasMaxValue; private set => _hasMaxValue = value; }

    [SerializeField] private float _value;
    public float Value { get => _value; private set => _value = value; }
    [SerializeField] private float _maxValue;
    public float MaxValue { get => _maxValue; private set => _maxValue = value; }

    public float OriginalValue { get; private set; }
    public float OriginalMaxValue { get; private set; }

    /// <summary>
    /// Событие при изменении текущего значения
    /// </summary>
    public event Action<NumericAttrib> OnValueChanged;

    public NumericAttrib(float value, bool hasMaxValue = true)
    {
        HasMaxValue = hasMaxValue;

        Value = OriginalValue = value;
        if (HasMaxValue)
            MaxValue = OriginalMaxValue = value;
    }

    public NumericAttrib(float value, float maxValue)
    {
        Value = OriginalValue = value;
        MaxValue = OriginalMaxValue = maxValue;
    }

    public NumericAttrib()
    {
        Value = OriginalValue = MaxValue = OriginalMaxValue = 1;
    }

    /// <returns>
    /// Пустое ли текущее значение
    /// </returns>
    public bool IsValueEmpty() => Value <= 0;

    /// <returns>
    /// Заполнено ли текущее значение до максимума
    /// </returns>
    public bool IsValueFull() => Value == MaxValue;

    /// <summary>
    /// Увеличивает значение на значение параметра
    /// </summary>
    /// <param name="value">Число на которое надо увеличить</param>
    public void IncreaseValue(float value = 1)
    {
        if (HasMaxValue)
            Value = Value + value > MaxValue
                ? MaxValue
                : Value + value;
        else
            Value += value;

        OnValueChanged?.Invoke(this);
    }

    /// <summary>
    /// Уменьшает значение на значение параметра
    /// </summary>
    /// <param name="value">Число на которое надо уменьшить</param>
    public void DecreaseValue(float value = 1)
    {
        if (IsValueEmpty())
            return;

        Value = Value - value < 0
            ? 0
            : Value - value;

        OnValueChanged?.Invoke(this);
    }

    /// <summary>
    /// Заполняет текущее значение до максимального
    /// </summary>
    public void FillToMax()
    {
        if (!HasMaxValue)
            return;

        Value = MaxValue;

        OnValueChanged?.Invoke(this);
    }

    /// <summary>
    /// Очищает текущее значение
    /// </summary>
    public void FillEmpty()
    {
        Value = 0;

        OnValueChanged?.Invoke(this);
    }

    /// <summary>
    /// Назначает макимальное значение на значение параметра
    /// </summary>
    /// <param name="maxValue">Значение на которое надо изменить</param>
    public void SetMax(float maxValue)
    {
        MaxValue = HasMaxValue
            ? maxValue
            : 0;
    }

    /// <summary>
    /// Сбрасывает значения до первоначальных
    /// </summary>
    public void ResetAttribute()
    {
        MaxValue = OriginalMaxValue;
        Value = OriginalValue;

        OnValueChanged?.Invoke(this);
    }

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
                default: Debug.LogException(new IndexOutOfRangeException("Максимальное значение индекса равняется двум!")); return -1;
            }
        }
        set
        {
            switch (index)
            {
                case 0: Value = value; break;
                case 1: MaxValue = value; break;
                case 2: OriginalValue = value; break;
                default: Debug.LogException(new IndexOutOfRangeException("Максимальное значение индекса равняется двум!")); break;
            }
        }
    }

    public static NumericAttrib operator !(NumericAttrib attrib)
    {
        attrib.Value = attrib.Value >= attrib.MaxValue / 2 ? 0 : attrib.MaxValue;

        return attrib;
    }

    public static NumericAttrib operator --(NumericAttrib attrib)
    {
        attrib.DecreaseValue();
        return attrib;
    }
    public static NumericAttrib operator -(NumericAttrib attrib1, NumericAttrib attrib2)
    {
        attrib1.DecreaseValue(attrib2.Value);
        return attrib1;
    }

    public static NumericAttrib operator ++(NumericAttrib attrib)
    {
        attrib.IncreaseValue();
        return attrib;
    }
    public static NumericAttrib operator +(NumericAttrib attrib1, NumericAttrib attrib2)
    {
        attrib1.IncreaseValue(attrib2.Value);
        return attrib1;
    }

    public static implicit operator NumericAttrib(float v) => new NumericAttrib(v);
    public static implicit operator float(NumericAttrib v) => v.Value;

    public static implicit operator string(NumericAttrib v) => v.ToString();
    public override string ToString() => $"{Value} / {MaxValue}";

    #endregion
}
