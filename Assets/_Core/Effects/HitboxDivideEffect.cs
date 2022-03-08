/// <summary>
/// Логика эфекта делителя хитбокса
/// </summary>
public class HitboxDivideEffect : BaseEffect
{
    private float _lastHitbox;

    public override void StartEffect()
    {
        _lastHitbox = _target.HitboxRange;

        _target.HitboxRange.DecreaseValue(GetEffectValue());
    }

    public override void StopEffect()
    {
        _target.HitboxRange.IncreaseValue(GetEffectValue());
    }

    /// <returns>
    /// Значение хитбокса с учетом эфекта
    /// </returns>
    private float GetEffectValue() => _lastHitbox / ((HitboxDivideEffectPreferences)GetPreferences()).DivideValue;
}
