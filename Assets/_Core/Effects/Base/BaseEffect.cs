/// <summary>
/// �������� ������ ������
/// </summary>
public abstract class BaseEffect
{
    public bool IsEndless { get; private set; }

    protected BaseUnit _target;
    protected BaseEffectPreferences _effectPreferences;

    /// <summary>
    /// ��������� ����� �� ����
    /// </summary>
    /// <param name="effectPreferences">��������� ������</param>
    /// <param name="target">����</param>
    public BaseEffect Instantiate(BaseEffectPreferences effectPreferences, BaseUnit target, bool isEndless)
    {
        _target = target;
        _effectPreferences = effectPreferences;
        IsEndless = isEndless;

        _target.GetComponent<EffectSystem>().AddEffect(this);

        return this;
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
