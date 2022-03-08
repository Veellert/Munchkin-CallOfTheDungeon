/// <summary>
/// Родитель логики эфекта
/// </summary>
public abstract class BaseEffect
{
    protected BaseUnit _target;
    protected BaseEffectPreferences _effectPreferences;

    /// <summary>
    /// Добавляет эфект на цель
    /// </summary>
    /// <param name="effectPreferences">Настройки эфекта</param>
    /// <param name="target">Цель</param>
    public void Instantiate(BaseEffectPreferences effectPreferences, BaseUnit target)
    {
        _target = target;
        _effectPreferences = effectPreferences;

        _target.GetComponent<EffectSystem>().AddEffect(this);
    }

    /// <summary>
    /// Запускает эфект
    /// </summary>
    public abstract void StartEffect();
    /// <summary>
    /// Останавливает эфект
    /// </summary>
    public abstract void StopEffect();

    /// <returns>
    /// Текущие настройки эфекта
    /// </returns>
    public BaseEffectPreferences GetPreferences() => _effectPreferences;
}
