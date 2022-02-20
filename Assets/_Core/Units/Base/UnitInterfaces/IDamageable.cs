/// <summary>
/// Может умереть
/// </summary>
public interface IDamageable
{
    NumericAttrib HP { get; }

    /// <summary>
    /// Умирает
    /// </summary>
    void Die();
    /// <summary>
    /// Получает урон
    /// </summary>
    /// <param name="damageAmount">Урон</param>
    void ReceiveDamage(float damageAmount);
    /// <summary>
    /// Восстанавливает здоровье на определенное кол-во единиц
    /// </summary>
    /// <param name="healAmount">Единицы здоровья</param>
    void Heal(float healAmount);
}
