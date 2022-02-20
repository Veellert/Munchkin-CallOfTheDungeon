/// <summary>
/// Может атаковать
/// </summary>
public interface IDamager
{
    /// <summary>
    /// Атакует цель
    /// </summary>
    /// <param name="target">Цель</param>
    /// <param name="damage">Урон который цель получит</param>
    void AttackTo(IDamageable target, float damage);
}
