/// <summary>
/// ����� ������ � ��������� ���������
/// </summary>
public interface IIdleMovable
{
    /// <summary>
    /// ������������ �� �����
    /// </summary>
    void StayOnPoint();
    /// <summary>
    /// ������� �����
    /// </summary>
    void RemovePoint();
    /// <summary>
    /// ���������������� �����
    /// </summary>
    void InitPoint();
}
