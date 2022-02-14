using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику игрока
/// </summary>
public class Player : BaseUnit, IDamager, IDamageable
{
    public static Player Instance { get; set; }


    public NumericAttrib DropCrystals { get; private set; }

    [Header("Damager Attribs")]
    [SerializeField] private NumericAttrib _damage = new NumericAttrib(8, 100);
    public NumericAttrib Damage { get => _damage; set => _damage = value; } // private set
    [SerializeField] private NumericAttrib _attackRange = new TileHalf(0.7f).Value;
    public NumericAttrib AttackRange { get => _attackRange; private set => _attackRange = value; }
    [SerializeField] private NumericAttrib _attackCooldown = 0.5f;
    public NumericAttrib AttackCooldown { get => _attackCooldown; private set => _attackCooldown = value; }

    [Header("Damageable Attribs")]
    [SerializeField] private NumericAttrib _hp = 100;
    public NumericAttrib HP { get => _hp; private set => _hp = value; }

    [Header("Player Attribs")]
    [SerializeField] private NumericAttrib _dodgeForce = new NumericAttrib(6, 10);
    public NumericAttrib DodgeForce { get => _dodgeForce; private set => _dodgeForce = value; }
    [SerializeField] private NumericAttrib _dodgeCooldown = new NumericAttrib(2, 5);
    public NumericAttrib DodgeCooldown { get => _dodgeCooldown; private set => _dodgeCooldown = value; }

    public bool IsDead => HP.IsValueEmpty();
    public bool CanDodge => DodgeCooldown.IsValueEmpty();
    public bool CanAttack => AttackCooldown.IsValueEmpty();


    private readonly UnitState _dodgeState = new UnitState("Dodge");
    private readonly UnitState _attackState = new UnitState("Attack");
    private readonly UnitState _duringAttackState = new UnitState("DuringAttack");
    private readonly UnitState _dieState = new UnitState("Die");


    private NumericAttrib _dodgeImpulse;

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        base.Start();

        DropCrystals = new NumericAttrib(0, false);
        _lastMovementDirection = Vector2.right;
        _dodgeImpulse = new NumericAttrib(DodgeForce);
    }

    private void FixedUpdate()
    {
        CheckAllCooldowns();
        StateMachine.ExecuteCurrent();
    }

    public void Die() => StateMachine.EnterTo(_dieState);
    public void GetDamage(float damageAmount)
    {
        HP -= damageAmount;
    }
    public void Heal(float healAmount)
    {
        HP += healAmount;
    }

    public void AttackInput()
    {
        if (CanAttack && StateMachine.IsCurrent(_defaultState))
            StateMachine.EnterTo(_attackState);
    }
    public void AttackHandler()
    {
        var mouseDirection = Direction2D.GetMouseDirection();
        
        // position + direction * offset
        _tempAttackPosition = (Vector2)transform.position + mouseDirection * new TileHalf();

        SetDirection(mouseDirection);
        VisualizeDirection(mouseDirection.x);

        StateMachine.EnterTo(_duringAttackState);
    }

    protected override void ReleaseAttack(IDamageable target, float damage)
    {
        if (StateMachine.IsCurrent(_dodgeState))
            return;

        base.ReleaseAttack(target, damage);
        AttackCooldown.FillToMax();
        StateMachine.EnterTo(_defaultState);
    }

    /// <summary>
    /// Начать рывок (кувырок)
    /// </summary>
    private void DodgeInput(InputButton obj)
    {
        if (CanDodge && (StateMachine.IsCurrent(_defaultState) || StateMachine.IsCurrent(_duringAttackState)))
            StateMachine.EnterTo(_dodgeState);
    }
    /// <summary>
    /// Обработка рывка (кувырка)
    /// </summary>
    private void DodgeHandler()
    {
        Move(_lastMovementDirection * _dodgeImpulse);
        _dodgeImpulse -= _dodgeImpulse.MaxValue * Time.deltaTime;

        _animation.DODGE(() => { StateMachine.EnterTo(_defaultState); });
    }

    /// <summary>
    /// Обработка бега
    /// </summary>
    private void RunHandler()
    {
        Move(_movementDirection * Speed);
        _animation.RUNNING(_movementDirection);
    }

    protected override void CheckAllCooldowns()
    {
        AttackCooldown -= Time.deltaTime;
        DodgeCooldown -= Time.deltaTime;
    }
    protected override void GetRequiredComponents()
    {

    }
    protected override void SubscribeOnEvents()
    {
        InputObserver.Instance._dodge.OnButtonUse += DodgeInput;
        InputObserver.Instance.OnLeftMouseButton += AttackInput;

        HP.OnValueChanged += HP_OnValueChanged;
    }
    protected override void InitializeStates()
    {
        StateMachine.AddRange(new[]
        {
            _dodgeState,
            _attackState,
            _duringAttackState,
            _dieState,
        });

        StateMachine.InitializeState(_defaultState,
            onExecute: ExecuteDefault);

        StateMachine.InitializeState(_dodgeState,
            onExecute: ExecuteDodge,
            onEnter: EnterDodge);
        
        StateMachine.InitializeState(_attackState,
            onExecute: ExecuteAttack,
            onEnter: EnterAttack);
        
        StateMachine.InitializeState(_duringAttackState,
            onExecute: ExecuteDuringAttack,
            onEnter: EnterDuringAttack);

        StateMachine.InitializeState(_dieState,
            onEnter: EnterDie);
    }

    /// <summary>
    /// При начале рывка (кувырка)
    /// </summary>
    private void EnterDodge()
    {
        _dodgeImpulse.FillToMax();
        DodgeCooldown.FillToMax();
    }
    /// <summary>
    /// При начале проведения атаки
    /// </summary>
    private void EnterDuringAttack()
    {
        _animation.ATTACK(() =>
        {
            ReleaseAttack(Monster.GetClosestMonster(_tempAttackPosition, AttackRange), Damage);
        });
    }
    /// <summary>
    /// При начале атаки
    /// </summary>
    private void EnterAttack()
    {
        ResetVelocity();
    }
    /// <summary>
    /// При начале смерти
    /// </summary>
    private void EnterDie()
    {
        ResetVelocity();
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<DirectionStatementVisualizer>().UnsubscribeChangingForPlayer();
        _animation.DIE();
    }

    /// <summary>
    /// Логика проведения атаки
    /// </summary>
    private void ExecuteDuringAttack()
    {
        SetDirection(InputObserver.Instance.GetInputDirection(), false);
    }
    /// <summary>
    /// Логика атаки
    /// </summary>
    private void ExecuteAttack()
    {
        AttackHandler();
    } 
    /// <summary>
    /// Логика рывка (кувырка)
    /// </summary>
    private void ExecuteDodge()
    {
        DodgeHandler();
    }
    /// <summary>
    /// Обычная логика
    /// </summary>
    private void ExecuteDefault()
    {
        SetDirection(InputObserver.Instance.GetInputDirection());
        RunHandler();
    }

    /// <summary>
    /// Событие при изменении здоровья
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void HP_OnValueChanged(NumericAttrib hp)
    {
        if (IsDead && !StateMachine.IsCurrent(_dieState))
            Die();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_tempAttackPosition, AttackRange);
    }
}
