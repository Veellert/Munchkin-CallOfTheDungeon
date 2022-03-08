/// <summary>
/// �������� ������ ������
/// </summary>
public abstract class BaseEffect
{
    protected BaseUnit _target;
    protected BaseEffectPreferences _effectPreferences;

    /// <summary>
    /// ��������� ����� �� ����
    /// </summary>
    /// <param name="effectPreferences">��������� ������</param>
    /// <param name="target">����</param>
    public void Instantiate(BaseEffectPreferences effectPreferences, BaseUnit target)
    {
        _target = target;
        _effectPreferences = effectPreferences;

        _target.GetComponent<EffectSystem>().AddEffect(this);
    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    public abstract void StartEffect();
    /// <summary>
    /// ������������� �����
    /// </summary>
    public abstract void StopEffect();

    /// <returns>
    /// ������� ��������� ������
    /// </returns>
    public BaseEffectPreferences GetPreferences() => _effectPreferences;
}
