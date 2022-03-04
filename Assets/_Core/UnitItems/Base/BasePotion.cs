/// <summary>
/// Компонент родитель отвечающий за логику зелья
/// </summary>
public abstract class BasePotion : BaseItem
{
    //===>> Attributes & Properties <<===\\

    public NumericAttrib UsagesCount { get; protected set; }

    protected bool CanUse => !UsagesCount.IsValueEmpty();

    //===>> Components & Fields <<===\\

    protected bool _isUsingNow;

    //===>> Unity <<===\\

    private void Start()
    {
        UsagesCount = new NumericAttrib(((PotionPreferences)_basePreferences).MaxUsageCount);
    }

    //===>> Important Methods <<===\\

    protected override void Use()
    {
        UsagesCount--;
        _isUsingNow = true;
    }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Использование зелья на цели
    /// </summary>
    /// <param name="target">Цель</param>
    public void UsePotionOn(BaseUnit target)
    {
        if (_isUsingNow || !CanUse)
            return;

        SetTarget(target);
        Use();
    }
}
