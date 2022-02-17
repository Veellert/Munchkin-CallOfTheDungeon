using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику зелья с временным эффектом
/// </summary>
public abstract class TemporaryPotion : BasePotion
{
    public float EffectDuration => _temporaryPotionPreferences.EffectDuration;

    private TemporaryPotionPreferences _temporaryPotionPreferences;

    /// <summary>
    /// Запускает эффект от зелья
    /// </summary>
    protected abstract void StartPotionEffect();
    /// <summary>
    /// Заканчивает эффект от зелья
    /// </summary>
    protected abstract void StopPotionEffect();

    protected override void Use()
    {
        base.Use();
        _temporaryPotionPreferences = (TemporaryPotionPreferences)_basePreferences;

        StartPotionEffect();
        Invoke(nameof(StopEffect), EffectDuration);
    }

    /// <summary>
    /// Остановка эффекта
    /// </summary>
    private void StopEffect()
    {
        _isUsingNow = false;
        StopPotionEffect();
        ClearTarget();
    }
}
