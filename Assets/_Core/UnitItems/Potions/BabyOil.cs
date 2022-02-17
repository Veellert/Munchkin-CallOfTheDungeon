/// <summary>
/// Класс отвечающий за логику зелья "Детское масло"
/// </summary>
public class BabyOil : TemporaryPotion
{
    protected override void StartPotionEffect()
    {
        _target.HitboxDistance.DecreaseValue(_target.HitboxDistance / 3);
    }

    protected override void StopPotionEffect()
    {
        _target.HitboxDistance.FillToMax();
    }
}
