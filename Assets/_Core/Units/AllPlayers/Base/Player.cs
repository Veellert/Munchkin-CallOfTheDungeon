using UnityEngine;

/// <summary>
/// Компонент отвечающий за базовую логику игрока
/// </summary>
[RequireComponent(typeof(PlayerInventory), typeof(PlayerEquipmentInventory))]
public class Player : MobileUnit, IDamager, IDamageable
{
    public static Player Instance { get; set; }

    //===>> Attributes & Properties <<===\\

    public ePlayerGender Gender { get; protected set; }

    public NumericAttrib Damage { get; protected set; }
    public NumericAttrib AttackRange { get; protected set; }
    public NumericAttrib AttackDistance { get; protected set; }
    public NumericAttrib AttackCooldown { get; protected set; }
    public bool CanAttack => AttackCooldown.IsValueEmpty();

    public NumericAttrib DodgeForce { get; protected set; }
    public NumericAttrib DodgeImpulse { get; protected set; }
    public bool IsDodging => !DodgeImpulse.IsValueEmpty();
    public NumericAttrib DodgeCooldown { get; protected set; }
    public bool CanDodge => DodgeCooldown.IsValueEmpty();

    public PlayerInventory Inventory { get; protected set; }
    public PlayerEquipmentInventory EquipmentInventory { get; protected set; }

    public bool IsInvisibleForMonster { get; protected set; }

    protected PlayerPreferences PlayerPreferences => (PlayerPreferences)_preferences;

    //===>> Components & Fields <<===\\

    protected Vector2 _tempAttackPosition;

    //===>> Unity <<===\\

    protected override void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        base.Awake();
    }

    //===>> Important Methods <<===\\

    protected override void GetRequiredComponents()
    {
        base.GetRequiredComponents();

        Inventory = GetComponent<PlayerInventory>();
        EquipmentInventory = GetComponent<PlayerEquipmentInventory>();
    }
    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        IsInvisibleForMonster = false;

        Damage = PlayerPreferences.Damage;
        AttackRange = PlayerPreferences.AttackRange;
        AttackDistance = PlayerPreferences.AttackDistance;
        AttackCooldown = PlayerPreferences.AttackCooldown;

        Gender = PlayerPreferences.Gender;

        DodgeForce = PlayerPreferences.DodgeForce;
        DodgeCooldown = PlayerPreferences.DodgeCooldown;
        DodgeImpulse = new NumericAttrib(0, DodgeForce);

        _tempAttackPosition = transform.position + Vector3.right;

        HP.OnValueChanged += OnHPChanged;
        _directionRenderer.SubscribeChangingForPlayer();
    }
    protected override void InitializeStateMachine()
    {
        _stateMachine = new PlayerStateMachine();

        ((PlayerStateMachine)_stateMachine).TransitToDefault();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void AttackTo(IDamageable target, float damage) => target?.ReceiveDamage(damage);

    public virtual void Die() => ((PlayerStateMachine)_stateMachine).TransitToDie();

    public virtual void ReceiveDamage(float damageAmount) => HP -= damageAmount;

    public virtual void Heal(float healAmount) => HP += healAmount;

    //===>> Public Methods <<===\\

    /// <summary>
    /// Устанавливает невидимость
    /// </summary>
    /// <param name="isEnabled">Переключатель</param>
    public void SetIvisibility(bool isEnabled) => IsInvisibleForMonster = isEnabled;

    /// <returns>
    /// Временную позицию атаки
    /// </returns>
    public Vector2 GetTempAttackPosition() => _tempAttackPosition;

    /// <summary>
    /// Устанавливает временную позицию атаки
    /// </summary>
    /// <param name="position">Позиция атаки</param>
    public void SetTempAttackPosition(Vector2 position) => _tempAttackPosition = position;

    /// <summary>
    /// Движение по направлению
    /// </summary>
    /// <param name="direction">Направление</param>
    public void Move(Vector2 direction) => _rigBody.velocity = direction;
    /// <summary>
    /// Сброс движения
    /// </summary>
    public void ResetVelocity() => Move(Vector2.zero);

    //===>> On Events <<===\\

    /// <summary>
    /// Событие при нажатии на кнопку рывка
    /// </summary>
    /// <param name="obj">Нажатая кнопка</param>
    public void DodgeInput(InputButton obj)
    {
        if (CanDodge)
            ((PlayerStateMachine)_stateMachine).TransitToDodge();
    }

    /// <summary>
    /// Событие при нажатии на левую кнопку мыши
    /// </summary>
    /// <param name="obj">Нажатая кнопка</param>
    public void AttackInput()
    {
        if (CanAttack)
            ((PlayerStateMachine)_stateMachine).TransitToAttack();
    }

    /// <summary>
    /// Событие при изменении здоровья
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void OnHPChanged(NumericAttrib hp)
    {
        if (IsDead)
            Die();
    }

    //===>> Gizmos <<===\\

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        #region AttackDistance

        if(AttackDistance != null)
            Gizmos.DrawWireSphere(transform.position, AttackDistance.TileHalfed());
        else
            Gizmos.DrawWireSphere(transform.position, PlayerPreferences.AttackDistance.TileHalfed());

        #endregion
        Gizmos.color = Color.white;
        #region Attack

        if(AttackRange != null)
            Gizmos.DrawWireSphere(_tempAttackPosition, AttackRange);
        else
            Gizmos.DrawWireSphere(transform.position + Vector3.right, PlayerPreferences.AttackRange);

        #endregion
    }
}
