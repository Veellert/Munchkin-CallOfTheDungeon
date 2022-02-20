using UnityEngine;

/// <summary>
/// Класс отвечающий за логику зелья "Детское масло"
/// </summary>
public class BabyOil : TemporaryPotion
{
    //===>> Inspector <<===\\

    [SerializeField] private float _divideHitboxValue = 3;

    //===>> Components & Fields <<===\\

    private BaseUnit _unitTarget;

    //===>> Important Methods <<===\\

    protected override void SetTarget(BaseUnit target)
    {
        base.SetTarget(target);

        if (_target)
            _unitTarget = _target.GetComponent<BaseUnit>();
    }

    protected override void StartPotionEffect()
    {
        _unitTarget.HitboxRange.DecreaseValue(_unitTarget.HitboxRange / _divideHitboxValue);
    }

    protected override void StopPotionEffect()
    {
        _unitTarget.HitboxRange.FillToMax();
    }
}
