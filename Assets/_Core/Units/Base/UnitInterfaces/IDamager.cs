/// <summary>
/// ����� ���������
/// </summary>
public interface IDamager
{
    /// <summary>
    /// ������� ����
    /// </summary>
    /// <param name="target">����</param>
    /// <param name="damage">���� ������� ���� �������</param>
    void AttackTo(IDamageable target, float damage);
}
