using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки эфекта полиморфа
/// </summary>
[CreateAssetMenu(fileName = "PollymorphEffect", menuName = "Effects/Pollymorph/Create Pollymorph Effect", order = 0)]
public class PollymorphEffectPreferences : BaseEffectPreferences
{
    [SerializeField] protected PollymorphMonster _pollymorphMonster;
    public PollymorphMonster PollymorphMonster => _pollymorphMonster;

    public override BaseEffect CreateEffectFor(BaseUnit target, bool isEndless = false)
        => new PollymorphEffect().Instantiate(this, target, isEndless);

    protected override eEffectType GetEffectType()
        => eEffectType.Pollymorph;
}