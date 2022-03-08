using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки эфекта полиморфа
/// </summary>
[CreateAssetMenu(fileName = "PollymorphEffect", menuName = "Effects/Pollymorph/Create Pollymorph Effect", order = 0)]
public class PollymorphEffectPreferences : BaseEffectPreferences
{
    [SerializeField] protected PollymorphMonster _pollymorphMonster;
    public PollymorphMonster PollymorphMonster => _pollymorphMonster;

    public override void CreateEffectFor(BaseUnit target)
    {
        new PollymorphEffect().Instantiate(this, target);
    }

    protected override eEffectType GetEffectType()
        => eEffectType.Pollymorph;
}