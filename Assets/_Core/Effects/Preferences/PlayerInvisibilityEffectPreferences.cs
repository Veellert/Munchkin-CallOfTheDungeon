using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки эфекта невидимости игрока
/// </summary>
[CreateAssetMenu(fileName = "PlayerInvisibilityEffect", menuName = "Effects/Invisibility/Create Player Invisibility Effect", order = 0)]
public class PlayerInvisibilityEffectPreferences : BaseEffectPreferences
{
    public override BaseEffect CreateEffectFor(BaseUnit target, bool isEndless = false)
        => new PlayerInvisibilityEffect().Instantiate(this, target, isEndless);

    protected override eEffectType GetEffectType()
        => eEffectType.Invisibility;
}
