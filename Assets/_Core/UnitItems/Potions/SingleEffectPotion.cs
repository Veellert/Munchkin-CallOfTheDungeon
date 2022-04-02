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
        if(_target)
            _mainEffect.CreateEffectFor(_target);
    }
}
