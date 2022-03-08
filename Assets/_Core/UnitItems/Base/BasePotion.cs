/// <summary>
/// Компонент родитель отвечающий за логику зелья
/// </summary>
public abstract class BasePotion : BaseItem
{
    //===>> Attributes & Properties <<===\\

    public NumericAttrib UsagesCount { get; protected set; }
    protected bool CanUse => !UsagesCount.IsValueEmpty();

    //===>> Unity <<===\\

    private void Start()
    {
        UsagesCount = new NumericAttrib(((PotionPreferences)_basePreferences).MaxUsageCount);
    }

    //===>> Important Methods <<===\\

    /// <summary>
    /// Инициализирует эфекты зелья
    /// </summary>
    protected abstract void InitializeEffects();

    protected override void Use()
    {
        UsagesCount--;
        InitializeEffects();
    }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Использование зелья на цели
    /// </summary>
    /// <param name="target">Цель</param>
    public void UsePotionOn(BaseUnit target)
    {
        SetTarget(target);
        Use();

        if (!CanUse)
            Destroy(gameObject);
    }
}
