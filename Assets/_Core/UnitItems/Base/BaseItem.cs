using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику предмета
/// </summary>
[RequireComponent(typeof(PSBRenderer))]
public abstract class BaseItem : MonoBehaviour
{
    [Header("Item Preferences")]
    [SerializeField] protected ItemPreferences _basePreferences;

    protected BaseUnit _target;
    protected PSBRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<PSBRenderer>();
    }

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

    /// <summary>
    /// Устанавливает цель
    /// </summary>
    /// <param name="target">Цель</param>
    protected void SetTarget(BaseUnit target) => _target = target;
    /// <summary>
    /// Обнуляет цель
    /// </summary>
    protected void ClearTarget() => SetTarget(null);
}
