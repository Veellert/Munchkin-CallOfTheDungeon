/// <summary>
/// Логика эфекта невидимости игрока
/// </summary>
public class PlayerInvisibilityEffect : BaseEffect
{
    public override void StartEffect()
    {
        SetInvisibility(true);
        VisualizeModelTransparency();
    }

    public override void StopEffect()
    {
        SetInvisibility(false);
        ResetModelTransparency();
    }

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
