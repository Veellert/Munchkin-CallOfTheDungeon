using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����� <see cref="SmileyFaceBoss"/>
/// </summary>
public enum ePhaseSmileyFace
{
    Default,
    Angry,
}

/// <summary>
/// ��������� ���������� �� ������ 1 ����� 1 ������ "�������"
/// </summary>
[RequireComponent(typeof(SkinChanger))]
public class SmileyFaceBoss : Monster, IBoss<ePhaseSmileyFace>
{
    public static UnitAttrib minionCount = new UnitAttrib(0, 5);

    [SerializeField] private SmileyFaceBossMinion _minion;

    #region Attrib

    [SerializeField] private ePhaseSmileyFace _currentBossPhase;
    public ePhaseSmileyFace CurrentBossPhase { get => _currentBossPhase; set => _currentBossPhase = value; }
    
    [SerializeField] private UnitAttrib _spawnMinionDelay = 8;
    public UnitAttrib SpawnMinionDelay { get => _spawnMinionDelay; set => _spawnMinionDelay = value; }

    #endregion

    private SkinChanger _skinner;
    private bool _isBossHalfHP;
    private bool _canSpecialAttack = true;

    protected override void Start()
    {
        base.Start();

        _skinner = GetComponent<SkinChanger>();
        HP.OnValueChanged += CheckBossHP;
    }

    /// <summary>
    /// ������� �� �������� �������� �����
    /// </summary>
    private void CheckBossHP(object sender, System.EventArgs e)
    {
        _isBossHalfHP = HP.Value <= HP.MaxValue / 2;

        if (_isBossHalfHP && _canSpecialAttack)
            InputSpecialAttack();

        if (HP.Value <= HP.MaxValue / 4)
            _state = eState.Default;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!Player.Instance)
            return;

        CheckAttackCooldown();
        switch (_state)
        {
            case eState.Default:
                TryChase();
                SetDirection(transform.position);
                break;

            case eState.Chase:
                ChaseHandler();
                break;

            case eState.Attack:
                AttackHandler();
                break;

            case eState.SpecialAttack:
                SpecialAttackHandler();
                break;
        }
    }

    /// <summary>
    /// ������ �����
    /// </summary>
    public override void Die()
    {
        if(CurrentBossPhase == ePhaseSmileyFace.Angry)
            base.Die();
        else
            ChangeBossPhaseTo(ePhaseSmileyFace.Angry);
    }

    /// <summary>
    /// ���������� �����
    /// </summary>
    private void AttackHandler()
    {
        var attackRange = new TileHalf();

        if (CurrentBossPhase == ePhaseSmileyFace.Angry)
            attackRange = new TileHalf(2);

        base.AttackHandler(attackRange);
    }

    /// <summary>
    /// �������� �� ������ �����
    /// </summary>
    private void InputSpecialAttack()
    {
        SetDirection(transform.position);

        _canSpecialAttack = false;
        _state = eState.SpecialAttack;
    }

    /// <summary>
    /// ���������� ������ �����
    /// </summary>
    private void SpecialAttackHandler()
    {
        if (!IsInvoking("SpawnMinion") && minionCount.Value < minionCount.MaxValue)
            Invoke("SpawnMinion", SpawnMinionDelay);
    }

    /// <summary>
    /// ����� ������� �����
    /// </summary>
    private void SpawnMinion()
    {
        if (IsDead)
            return;

        var spawnDirection = (Player.Instance.transform.position - transform.position).normalized;
        var spawnPoint = transform.position + spawnDirection * new TileHalf(BtwTargetDistance);

        minionCount++;
        SmileyFaceBossMinion.Spawn(spawnPoint, _minion, this);
    }

    /// <summary>
    /// ������� ���� ����� �� ������
    /// </summary>
    /// <param name="bossPhase">���� �����</param>
    public void ChangeBossPhaseTo(ePhaseSmileyFace bossPhase)
    {
        if (CurrentBossPhase == bossPhase)
            return;

        CurrentBossPhase = bossPhase;
        _skinner.ChangeFullSkin(CurrentBossPhase);

        HP.FillToMax();
        Speed++;
        Damage += 5;
        SpawnMinionDelay -= 2;
        _canSpecialAttack = true;
    }
}
