/// <summary>
/// Родитель логики эфекта
/// </summary>
public abstract class BaseEffect
{
    public bool IsEndless { get; private set; }

    protected BaseUnit _target;
    protected BaseEffectPreferences _effectPreferences;

    /// <summary>
    /// Добавляет эфект на цель
    /// </summary>
    /// <param name="effectPreferences">Настройки эфекта</param>
    /// <param name="target">Цель</param>
    public BaseEffect Instantiate(BaseEffectPreferences effectPreferences, BaseUnit target, bool isEndless)
    {
        _target = target;
        _effectPreferences = effectPreferences;
        IsEndless = isEndless;

        _target.GetComponent<EffectSystem>().AddEffect(this);

        return this;
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
