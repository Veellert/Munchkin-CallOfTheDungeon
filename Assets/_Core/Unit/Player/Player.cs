using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику игрока
/// </summary>
[RequireComponent(typeof(AnimationCaller), typeof(Rigidbody2D), typeof(DirectionStatementManager))]
public class Player : MonoBehaviour, IUnit, IDamager, IDamageable
{
    public static Player Instance { get; set; }

    private Rigidbody2D _rigBody;
    private DirectionStatementManager _directionManager;
    private AnimationCaller _animation;

    #region Attrib

    public UnitAttrib DropCrystals = new UnitAttrib(0, false);

    [SerializeField] private string _unitName = "Безымянный игрок";
    public string UnitName { get => _unitName; set => _unitName = value; }
    [SerializeField] private UnitAttrib _speed = new UnitAttrib(5, 10);
    public UnitAttrib Speed { get => _speed; set => _speed = value; }
    [SerializeField] private float _hitboxDistance = 0.5f;
    public float HitboxDistance { get => _hitboxDistance; set => _hitboxDistance = value; }
    [SerializeField] private Vector2 _hitboxOffset = Vector2.zero;
    public Vector2 HitboxOffset { get => _hitboxOffset; set => _hitboxOffset = value; }

    [SerializeField] private UnitAttrib _dodgeForce = new UnitAttrib(6, 10);
    public UnitAttrib DodgeForce { get => _dodgeForce; set => _dodgeForce = value; }
    [SerializeField] private UnitAttrib _dodgeCooldown = new UnitAttrib(2, 5);
    public UnitAttrib DodgeCooldown { get => _dodgeCooldown; set => _dodgeCooldown = value; }

    [SerializeField] private UnitAttrib _damage = new UnitAttrib(8, 100);
    public UnitAttrib Damage { get => _damage; set => _damage = value; }
    [SerializeField] private UnitAttrib _attackCooldown = 0.5f;
    public UnitAttrib AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }

    [SerializeField] private UnitAttrib _hp = 100;
    public UnitAttrib HP { get => _hp; set => _hp = value; }
    public bool IsDead => HP.IsValueEmpty();

    #endregion

    private bool _isDisabled;
    private eState _state = eState.Default;
    private eState _lastState;

    /// <summary>
    /// Состояние игрока
    /// </summary>
    private enum eState
    {
        Disabled,
        Default,
        Dodge,
        Attack,
        DuringAttack,
        Die,
    }

    private Vector2 _movementDirection;
    private Vector2 _lastMovementDirection;

    private UnitAttrib _dodgeImpulse;

    private void Start()
    {
        #region PreLoad

        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        #endregion

        _rigBody = GetComponent<Rigidbody2D>();
        _directionManager = GetComponent<DirectionStatementManager>();
        _animation = GetComponent<AnimationCaller>();

        name = UnitName;
        _dodgeImpulse = new UnitAttrib(DodgeForce);
    }

    private void FixedUpdate()
    {
        CheckDodgeCooldown();
        CheckAttackCooldown();
        switch (_state)
        {
            case eState.Disabled:
                break;
                
            case eState.Default:
                SetDirection(GetInputDirection());
                RunHandler();
                DodgeInput();
                AttackInput();
                break;

            case eState.Dodge:
                DodgeHandler();
                break;

            case eState.Attack:
                AttackHandler(new TileHalf(0.7f));
                break;

            case eState.DuringAttack:
                SetDirection(GetInputDirection(), false);
                DodgeInput();
                break;
        }
    }

    public void SetDisableState(bool isDisable)
    {
        _isDisabled = isDisable;
        if (_state != eState.Disabled)
            _lastState = _state;

        if (isDisable)
            _state = eState.Disabled;
        else
            _state = _lastState;
    }

    /// <summary>
    /// Смерть игрока
    /// </summary>
    public void Die()
    {
        _state = eState.Die;
        _rigBody.velocity = Vector2.zero;
        GetComponent<Collider2D>().isTrigger = true;
        _animation.PlayDIE();
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    public void GetDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP.IsValueEmpty())
            Die();
    }

    /// <summary>
    /// Восстановление здоровья
    /// </summary>
    public void Heal(float healAmount)
    {
        HP += healAmount;
    }

    /// <summary>
    /// Проверка на атаку
    /// </summary>
    public void AttackInput()
    {
        if (Input.GetMouseButton(0) && AttackCooldown.IsValueEmpty())
        {
            if (_state != eState.Attack)
                _state = eState.Attack;

            Move(Vector2.zero);
            AttackCooldown.FillToMax();
        }
    }

    /// <summary>
    /// Обработка атаки
    /// </summary>
    public void AttackHandler(float attackRange)
    {
        var mouseDirection = (Direction2D.GetMousePosition() - transform.position).normalized;
        var attackPosition = transform.position + mouseDirection * new TileHalf();
        SetDirection(mouseDirection);

        // Visualize Gizmos
        _pos = attackPosition;
        _rad = attackRange;

        _state = eState.DuringAttack;

        _animation.PlayATTACK(() =>
        {
            if (_state != eState.Dodge)
            {
                Monster.GetClosestMonster(attackPosition, attackRange)?.GetDamage(Damage);

                if(!_isDisabled)
                    _state = eState.Default;
            }
        });
    }

    /// <summary>
    /// Проверка кулдауна атаки
    /// </summary>
    public void CheckAttackCooldown()
    {
        if (!AttackCooldown.IsValueEmpty())
            AttackCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Проверка кулдауна рывка (кувырка)
    /// </summary>
    private void CheckDodgeCooldown()
    {
        if (!DodgeCooldown.IsValueEmpty())
            DodgeCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Проверка на рывок (кувырок)
    /// </summary>
    private void DodgeInput()
    {
        if (Input.GetKey(KeyCode.Space) && DodgeCooldown.IsValueEmpty())
        {
            _state = eState.Dodge;

            _dodgeImpulse.FillToMax();
            DodgeCooldown.FillToMax();

            if (_lastMovementDirection == Vector2.zero)
                 _lastMovementDirection = Vector2.right;
        }
    }

    /// <summary>
    /// Обработка рывка (кувырка)
    /// </summary>
    private void DodgeHandler()
    {
        Move(_lastMovementDirection * _dodgeImpulse);
        _dodgeImpulse -= _dodgeImpulse.MaxValue * Time.deltaTime;

        _animation.PlayDODGE(() => { _state = eState.Default; });
    }

    /// <summary>
    /// Обработка бега
    /// </summary>
    private void RunHandler()
    {
        Move(_movementDirection * Speed);
        _animation.PlayRUNNING(_movementDirection);
    }

    /// <summary>
    /// Движение по направлению
    /// </summary>
    /// <param name="direction">Направление</param>
    private void Move(Vector2 direction) => _rigBody.velocity = direction;

    /// <summary>
    /// Устанавливает направление
    /// </summary>
    /// <param name="direction">Направление</param>
    /// <param name="needAnimation">Нужна ли анимация</param>
    private void SetDirection(Vector2 direction, bool needAnimation = true)
    {
        _movementDirection = direction;
        if (_movementDirection != Vector2.zero)
            _lastMovementDirection = _movementDirection;

        CheckDirection(needAnimation);
    }

    /// <summary>
    /// Проверяет направление
    /// </summary>
    /// <param name="needAnimation">Нужна ли анимация</param>
    private void CheckDirection(bool needAnimation)
    {
        _directionManager.ChangeDirection(_movementDirection);
        if(needAnimation)
            _animation.PlayIDLE(_movementDirection);
    }

    /// <summary>
    /// Проверка движения
    /// </summary>
    private Vector2 GetInputDirection()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        return new Vector2(xInput, yInput).normalized;
    }

    #region Gizmos

    Vector2 _pos = Vector2.zero;
    float _rad;

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(_pos, _rad);
        Gizmos.DrawWireSphere(_pos, new TileHalf(0.7f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)HitboxOffset, new TileHalf(HitboxDistance));
    }

    #endregion
}
