using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику экипировки оружия
/// </summary>
public class WeaponEquipment : BaseEquipment
{
    private WeaponPreferences WeaponPreferences => (WeaponPreferences)_basePreferences;

    private float _range;
    private float _cooldown;

    //===>> Important Methods <<===\\

    protected override void UnEquip()
    {
        base.UnEquip();

        Player.Instance.Damage.SetMax(Player.Instance.Damage.MaxValue - WeaponPreferences.AddictiveDamage);
        Player.Instance.Damage.DecreaseValue(WeaponPreferences.AddictiveDamage);

        Player.Instance.AttackRange.SetMax(_range);
        Player.Instance.AttackRange.FillToMax();

        Player.Instance.AttackCooldown.SetMax(_cooldown);
        Player.Instance.AttackCooldown.FillToMax();
    }
    protected override void Equip()
    {
        base.Equip();
        _range = Player.Instance.AttackRange;
        _cooldown = Player.Instance.AttackCooldown.MaxValue;

        Player.Instance.Damage.SetMax(Player.Instance.Damage.MaxValue + WeaponPreferences.AddictiveDamage);
        Player.Instance.Damage.IncreaseValue(WeaponPreferences.AddictiveDamage);

        float addictiveRange = _range - Player.Instance.AttackRange.OriginalValue;
        Player.Instance.AttackRange.SetMax(addictiveRange + WeaponPreferences.Range);
        Player.Instance.AttackRange.FillToMax();

        float addictiveCooldown = _cooldown - Player.Instance.AttackCooldown.OriginalMaxValue;
        Player.Instance.AttackCooldown.SetMax(addictiveCooldown + WeaponPreferences.Cooldown);
        Player.Instance.AttackCooldown.FillToMax();
    }

    //===>> Gizmos <<===\\

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        #region AttackDistance

        Gizmos.DrawWireSphere(transform.position, WeaponPreferences.Distance.TileHalfed());

        #endregion
        Gizmos.color = Color.white;
        #region Attack

        Gizmos.DrawWireSphere(transform.position + Vector3.right, WeaponPreferences.Range);

        #endregion
    }
}
