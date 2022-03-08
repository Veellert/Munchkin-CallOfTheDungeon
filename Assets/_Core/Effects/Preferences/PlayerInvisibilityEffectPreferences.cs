using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки эфекта невидимости игрока
/// </summary>
[CreateAssetMenu(fileName = "PlayerInvisibilityEffect", menuName = "Effects/Invisibility/Create Player Invisibility Effect", order = 0)]
public class PlayerInvisibilityEffectPreferences : BaseEffectPreferences
{
    public override void CreateEffectFor(BaseUnit target)
    {
        new PlayerInvisibilityEffect().Instantiate(this, target);
    }

    protected override eEffectType GetEffectType()
        => eEffectType.Invisibility;
}
