using Assets.DropSystem;
using UnityEngine;

/// <summary>
/// Логика полиморфного эфекта
/// </summary>
public class PollymorphEffect : BaseEffect
{
    public override void StartEffect()
    {
        Object.Instantiate(((PollymorphEffectPreferences)GetPreferences()).PollymorphMonster,
            _target.gameObject.transform.position, Quaternion.identity).
            InitializePollymorph(_target.GetComponent<DropSystem>(), _target.GetComponent<BaseMonster>());
    }

    public override void StopEffect()
    {

    }
}
