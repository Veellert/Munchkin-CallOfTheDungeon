using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки эфекта деления хитбокса
/// </summary>
[CreateAssetMenu(fileName = "HitboxDivideEffect", menuName = "Effects/Hitbox/Create Hitbox Divide Effect", order = 0)]
public class HitboxDivideEffectPreferences : BaseEffectPreferences
{
    [SerializeField] [Min(0.1f)] protected float _divideValue = 3;
    public float DivideValue => _divideValue;

    public override void CreateEffectFor(BaseUnit target)
    {
        new HitboxDivideEffect().Instantiate(this, target);
    }

    protected override eEffectType GetEffectType()
        => eEffectType.Hitbox;
}
