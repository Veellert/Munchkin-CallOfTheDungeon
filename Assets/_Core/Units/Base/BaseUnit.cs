using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Компонент родитель отвечающий за логику юнита
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(SortingGroup))]
[RequireComponent(typeof(PSBRenderer), typeof(AnimationCaller), typeof(EffectSystem))]
public abstract class BaseUnit : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Unit Preferences")]
    [SerializeField] protected UnitPreferences _preferences;
    [Header("Base Attribs")]
    [SerializeField] protected Vector2 _hitboxOffset;

    //===>> Attributes & Properties <<===\\

    public NumericAttrib HitboxRange { get; protected set; }
    public Vector2 HitboxOffset => _hitboxOffset;

    public NumericAttrib HP { get; protected set; }
    public bool IsDead => HP.IsValueEmpty();

    //===>> Components & Fields <<===\\

    protected Rigidbody2D _rigBody;
    protected PSBRenderer _renderer;
    protected AnimationCaller _animation;

    protected UnitStateMachine _stateMachine;

    //===>> Unity <<===\\

    protected virtual void Awake()
    {
        GetRequiredComponents();
        InitializeAttributes();
    }

    protected void Start()
    {
        InitializeStateMachine();
    }

    protected void FixedUpdate()
    {
        _stateMachine.ExecuteCurrent();
    }

    //===>> Important Methods <<===\\

    /// <summary>
    /// Инициализирует машину состояний
    /// </summary>
    protected abstract void InitializeStateMachine();

    /// <summary>
    /// Получает все требуемые компоненты
    /// </summary>
    protected virtual void GetRequiredComponents()
    {
        _rigBody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<AnimationCaller>();
        _renderer = GetComponent<PSBRenderer>();
    }

    /// <summary>
    /// Инициализирует разные атрибуты юнита
    /// </summary>
    protected virtual void InitializeAttributes()
    {
        name = _preferences.ID;
        HitboxRange = _preferences.HitboxRange;
        HP = _preferences.HP;
    }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Разрушение объекта юнита
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    //===>> Gizmos <<===\\

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        #region Hitbox

        Vector2 center = (Vector2)transform.position + HitboxOffset;
        if(HitboxRange != null)
            Gizmos.DrawWireSphere(center, HitboxRange.TileHalfed());
        else
            Gizmos.DrawWireSphere(center, _preferences.HitboxRange.TileHalfed());

        #endregion
    }
}
