using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику зелья
/// </summary>
public abstract class BasePotion : BaseItem
{
    public NumericAttrib UsagesCount { get; protected set; }

    protected bool CanUse => !UsagesCount.IsValueEmpty();

    protected bool _isUsingNow;

    private PotionPreferences _potionPreferences;

    private void Start()
    {
        _potionPreferences = (PotionPreferences)_basePreferences;
        UsagesCount = new NumericAttrib(_potionPreferences.MaxUsageCount);
    }

    protected override void Use()
    {
        UsagesCount--;
        _isUsingNow = true;
    }

    /// <summary>
    /// Использование зелья на цели
    /// </summary>
    /// <param name="target">Цель</param>
    public void UsePotionOn(BaseUnit target)
    {
        if (_isUsingNow || !CanUse)
            return;

        SetTarget(target);
        Use();
        if (!CanUse)
            Disappear();
    }
}
