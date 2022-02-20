/// <summary>
/// Класс отвечающий за логику зелья "Зелье невидимости"
/// </summary>
public class InvisibilityPotion : TemporaryPotion
{
    //===>> Important Methods <<===\\

    protected override void StartPotionEffect()
    {
        SetInvisibility(true);
        VisualizeModelTransparency();
    }

    protected override void StopPotionEffect()
    {
        SetInvisibility(false);
        ResetModelTransparency();
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// Устанавливает невидимость игрока для монстров
    /// </summary>
    /// <param name="isEnabled">Переключатель невидимости</param>
    private void SetInvisibility(bool isEnabled)
    {
        Player.Instance.SetIvisibility(isEnabled);
    }

    /// <summary>
    /// Визуализирует невидимость
    /// </summary>
    private void VisualizeModelTransparency()
    {
        Player.Instance.GetComponent<PSBRenderer>().SetAlpha(0.7f);
    }

    /// <summary>
    /// Восстанавливает прежнюю визуализацию невидимости
    /// </summary>
    private void ResetModelTransparency()
    {
        Player.Instance.GetComponent<PSBRenderer>().ResetAlpha();
    }
}
