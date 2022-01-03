using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePhaseSmileyFace
{
    Default,
    Angry,
}

[RequireComponent(typeof(SkinChanger))]
public class SmileyFaceBoss : Monster, IBoss<ePhaseSmileyFace>
{
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

    public override void Die()
    {
        if(CurrentBossPhase == ePhaseSmileyFace.Angry)
            base.Die();
        else
            ChangeBossFormTo(ePhaseSmileyFace.Angry);
    }

    private void AttackHandler()
    {
        var attackRange = new TileHalf();

        if (CurrentBossPhase == ePhaseSmileyFace.Angry)
            attackRange = new TileHalf(2);

        base.AttackHandler(attackRange);
    }

    private void InputSpecialAttack()
    {
        SetDirection(transform.position);

        _canSpecialAttack = false;
        _state = eState.SpecialAttack;
    }

    private void SpecialAttackHandler()
    {
        if (!IsInvoking("SpawnMinion"))
            Invoke("SpawnMinion", SpawnMinionDelay);
    }

    private void SpawnMinion()
    {
        var spawnDirection = (_chaseTarget.position - transform.position).normalized;
        var spawnPoint = transform.position + spawnDirection * new TileHalf(BtwTargetDistance);

        _minion.Spawn(spawnPoint);
    }

    public void ChangeBossFormTo(ePhaseSmileyFace bossForm)
    {
        if (CurrentBossPhase == bossForm)
            return;

        CurrentBossPhase = bossForm;
        _skinner.ChangeFullSkin(CurrentBossPhase);

        HP.FillToMax();
        Speed++;
        Damage += 5;
        SpawnMinionDelay = 6;
        _canSpecialAttack = true;
    }
}
