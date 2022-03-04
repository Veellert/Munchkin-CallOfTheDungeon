/// <summary>
/// Компонент родитель отвечающий за логику зелья с временным эффектом
/// </summary>
public abstract class TemporaryPotion : BasePotion
{
    //===>> Attributes & Properties <<===\\

    public float EffectDuration => ((TemporaryPotionPreferences)_basePreferences).EffectDuration;

    //===>> Important Methods <<===\\

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

        StartPotionEffect();
        Invoke(nameof(StopEffect), EffectDuration);
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// Остановка эффекта
    /// </summary>
    private void StopEffect()
    {
        _isUsingNow = false;
        StopPotionEffect();
        ClearTarget();
        Destroy(gameObject);
    }
}
