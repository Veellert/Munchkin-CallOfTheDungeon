using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику предмета
/// </summary>
[RequireComponent(typeof(PSBRenderer))]
public abstract class BaseItem : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Item Preferences")]
    [SerializeField] protected ItemPreferences _basePreferences;

    //===>> Components & Fields <<===\\

    protected PSBRenderer _renderer;

    protected BaseUnit _target;

    //===>> Unity <<===\\

    private void Awake()
    {
        _renderer = GetComponent<PSBRenderer>();
    }

    //===>> Important Methods <<===\\

    /// <summary>
    /// Локига использования предмета
    /// </summary>
    protected abstract void Use();

    /// <summary>
    /// Исчезновение предмета
    /// </summary>
    public virtual void Disappear()
    {
        _renderer.SetAlpha(0);
        Destroy(GetComponent<CapsuleCollider2D>());
    }

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
