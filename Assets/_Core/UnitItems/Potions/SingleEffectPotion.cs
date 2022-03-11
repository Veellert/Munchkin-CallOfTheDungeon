using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику зелья с одиночным эфектом
/// </summary>
public class SingleEffectPotion : BasePotion
{
    //===>> Inspector <<===\\

    [Header("Main Effect")]
    [SerializeField] protected BaseEffectPreferences _mainEffect;

    //===>> Important Methods <<===\\

    protected override void InitializeEffects()
    {
        _mainEffect.CreateEffectFor(_target);
    }
}
