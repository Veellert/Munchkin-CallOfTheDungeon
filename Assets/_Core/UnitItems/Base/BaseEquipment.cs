using System.Collections.Generic;

/// <summary>
/// Компонент родитель отвечающий за логику экипировки
/// </summary>
public abstract class BaseEquipment : BaseItem
{
    //===>> Attributes & Properties <<===\\

    public bool IsEquiped { get; protected set; }

    private List<BaseEffectPreferences> EffectList => ((EquipmentPreferences)_basePreferences).EffectList;
    private List<BaseEffect> _activeEffectList = new List<BaseEffect>();

    //===>> Important Methods <<===\\

    protected override void Use()
    {
        IsEquiped = !IsEquiped;
    }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Снять текущую экипировку
    /// </summary>
    public void UnEquipCurrent()
    {
        if (IsEquiped && _target)
            UnEquip();
    }

    /// <summary>
    /// Надеть экипировку на игрока
    /// </summary>
    public void EquipOn()
    {
        SetTarget(Player.Instance);

        if (!IsEquiped && _target)
            Equip();
    }

    /// <summary>
    /// Снять экипировку
    /// </summary>
    protected virtual void UnEquip()
    {
        Use();

        var effectSystem = _target.GetComponent<EffectSystem>();
        _activeEffectList.ForEach(effect =>
        {
            effectSystem.RemoveEffectImmediate(effect);
        });
        _activeEffectList.Clear();
    }
    /// <summary>
    /// Надеть экипировку
    /// </summary>
    protected virtual void Equip()
    {
        Use();

        EffectList.ForEach(effect =>
        {
            _activeEffectList.Add(effect.CreateEffectFor(_target, true));
        });
    }
}
