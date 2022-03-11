using Assets.PickUpSystem;
using Assets.UsingSystem;
using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику предмета
/// </summary>
[RequireComponent(typeof(PSBRenderer), typeof(PickUpItemSystem), typeof(UsingSystem))]
public abstract class BaseItem : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Item Preferences")]
    [SerializeField] protected ItemPreferences _basePreferences;

    //===>> Components & Fields <<===\\

    protected PSBRenderer _renderer;
    protected UsingSystem _usingSystem;

    protected BaseUnit _target;

    //===>> Unity <<===\\

    private void Awake()
    {
        _renderer = GetComponent<PSBRenderer>();
        _usingSystem = GetComponent<UsingSystem>();
    }

    //===>> Important Methods <<===\\

    /// <summary>
    /// Локига использования предмета
    /// </summary>
    protected abstract void Use();

    //===>> Public Methods <<===\\

    /// <summary>
    /// Использовать предмет
    /// </summary>
    public void UseItem() => _usingSystem.Use();
    /// <returns>
    /// Настройки предмета
    /// </returns>
    public ItemPreferences GetPreferences() => _basePreferences;

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// Устанавливает цель
    /// </summary>
    /// <param name="target">Цель</param>
    protected virtual void SetTarget(BaseUnit target) => _target = target;
    /// <summary>
    /// Обнуляет цель
    /// </summary>
    protected void ClearTarget() => SetTarget(null);
}
