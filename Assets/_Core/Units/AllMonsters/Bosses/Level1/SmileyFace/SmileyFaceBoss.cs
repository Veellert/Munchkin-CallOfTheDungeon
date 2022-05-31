using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику 1 босса 1 уровня "Смайлик"
/// </summary>
public partial class SmileyFaceBoss : Boss
{
    //===>> Inspector <<===\\

    [Header("Addictive Boss Phases")]
    [SerializeField] private BossPhase _angry;

    [Header("Boss Minion")]
    [SerializeField] private SmileyFaceMinion _minion;
    [SerializeField] private NumericAttrib _spawnMinionDelay = 8;
    [SerializeField] private float _maxMinionCount = 5;

    //===>> Attributes & Properties <<===\\

    public NumericAttrib MinionCount { get; private set; }
    public NumericAttrib SpawnMinionDelay { get => _spawnMinionDelay; private set => _spawnMinionDelay = value; }

    //===>> Important Methods <<===\\

    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        MinionCount = new NumericAttrib(0, _maxMinionCount);
        HP.OnValueChanged += OnHPHalf;
        _angry.OnPhaseChanged += OnAngryPhase;
    }
    protected override void InitializeStateMachine()
    {
        base.InitializeStateMachine();

        _stateMachine = new SmilyFaceBossStateMachine((AgressiveMonsterStateMachine)_stateMachine);
    }
    protected override void InitializeBossPhases()
    {
        base.InitializeBossPhases();

        AddBossPhase(_angry);
    }
    protected override void InitializePhasesScripts()
    {
        CreatePhasesScript(nameof(Die),
            defaultAction: base.Die,
            new List<(BossPhase phase, Action scriptAction)>
            {
                (DefaultBossPhase, () => ChangeBossPhaseTo(_angry)),
            });
    }

    //===>> Interfaces Methods <<===\\

    public override void Die()
    {
        CurrentBossPhase.InvokeScript(nameof(Die));
    }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Спавн миньона босса
    /// </summary>
    public void SpawnMinion()
    {
        if (IsDead)
            return;

        var spawnDirection = Direction2D.GetPlayerDirectionFrom(transform.position);
        spawnDirection *= BtwTargetDistance.TileHalfed();

        var spawnPoint = (Vector2)transform.position + spawnDirection;

        MinionCount++;
        _minion.Spawn(spawnPoint, _minion, this);
    }

    //===>> On Events <<===\\

    /// <summary>
    /// Событие на проверку здоровья босса
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    public void OnHPHalf(NumericAttrib hp)
    {
        if (hp.Value <= hp.MaxValue / 2 && hp.Value > hp.MaxValue / 4)
            ((SmilyFaceBossStateMachine)_stateMachine).TransitToSpecialAttack();
    }

    /// <summary>
    /// Событие на проверку здоровья босса
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    public void OnHpQuarter(NumericAttrib hp)
    {
        if (hp.Value <= hp.MaxValue / 4)
            ((SmilyFaceBossStateMachine)_stateMachine).TransitToDefault();
    }

    /// <summary>
    /// При переходе на фазу <see cref = "_angry" />
    /// </summary>
    /// <param name="obj">Переходная фаза</param>
    private void OnAngryPhase(BossPhase obj)
    {
        HP.FillToMax();
        Speed++;
        Damage += 5;
        AttackRange += new TileHalf();
        SpawnMinionDelay -= 2;
    }
}
