/// <summary>
/// Компонент отвечающий за логику экипировки брони
/// </summary>
public class ArmorEquipment : BaseEquipment
{
    private ArmorPreferences ArmorPreferences => (ArmorPreferences)_basePreferences;

    //===>> Important Methods <<===\\

    protected override void UnEquip()
    {
        base.UnEquip();

        Player.Instance.HP.SetMax(Player.Instance.HP.MaxValue - ArmorPreferences.AddictiveHP);
        Player.Instance.HP.DecreaseValue(ArmorPreferences.AddictiveHP);
    }
    protected override void Equip()
    {
        base.Equip();

        Player.Instance.HP.SetMax(Player.Instance.HP.MaxValue + ArmorPreferences.AddictiveHP);
        Player.Instance.HP.IncreaseValue(ArmorPreferences.AddictiveHP);
    }
}
