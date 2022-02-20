/// <summary>
/// ����� �������
/// </summary>
public interface IDamageable
{
    NumericAttrib HP { get; }

    /// <summary>
    /// �������
    /// </summary>
    void Die();
    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="damageAmount">����</param>
    void ReceiveDamage(float damageAmount);
    /// <summary>
    /// ��������������� �������� �� ������������ ���-�� ������
    /// </summary>
    /// <param name="healAmount">������� ��������</param>
    void Heal(float healAmount);
}
