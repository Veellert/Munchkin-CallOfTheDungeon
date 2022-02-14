using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику 1 босса 1 уровня "Смайлик"
/// </summary>
public class SmileyFaceBoss : Boss
{
    public static NumericAttrib _minionCount = new NumericAttrib(0, 5);

    [Header("Addictive Boss Phases")]
    [SerializeField] private BossPhase _angry;

    [Header("Boss Minion")]
    [SerializeField] private SmileyFaceMinion _minion;
    [SerializeField] private NumericAttrib _spawnMinionDelay = 8;
    public NumericAttrib SpawnMinionDelay { get => _spawnMinionDelay; private set => _spawnMinionDelay = value; }


    private readonly UnitState _specialAttackState = new UnitState("SpecialAttack");


    private bool _isBossHalfHP;
    private bool _canSpecialAttack = true;

    public override void Die()
    {
        _currentBossPhase.InvokeScript(nameof(Die));
    }

    /// <summary>
    /// Начать особую атаку
    /// </summary>
    private void InputSpecialAttack()
    {
        StateMachine.EnterTo(_specialAttackState);
    }
    /// <summary>
    /// Обработка особой атаки
    /// </summary>
    private void SpecialAttackHandler()
    {
        if(_minionCount.Value < _minionCount.MaxValue)
            InvokeLoop(nameof(SpawnMinion), SpawnMinionDelay);
    }

    /// <summary>
    /// Спавн миньона босса
    /// </summary>
    private void SpawnMinion()
    {
        if (IsDead || !StateMachine.IsCurrent(_specialAttackState))
            return;
        
        var spawnDirection = (Player.Instance.transform.position - transform.position).normalized;
        var spawnPoint = transform.position + spawnDirection * new TileHalf(BtwTargetDistance);

        _minionCount++;
        _minion.Spawn(spawnPoint, _minion, this);
    }

    protected override void InitBossPhases()
    {
        base.InitBossPhases();
        AddBossPhase(_angry);
    }
    protected override void InitPhasesScripts()
    {
        CreatePhasesScript(nameof(Die),
            defaultAction: base.Die,
            new List<(BossPhase phase, Action scriptAction)>
            {
                (_default, () => ChangeBossPhaseTo(_angry)),
            });
    }
    protected override void SubscribeOnEvents()
    {
        base.SubscribeOnEvents();

        HP.OnValueChanged += CheckBossHP;
        _angry.OnPhaseChanged += OnAngryPhase;
    }
    protected override void InitializeStates()
    {
        base.InitializeStates();
        StateMachine.Add(_specialAttackState);

        StateMachine.InitializeState(_defaultState,
            onExecute: ExecuteDefault);
        
        StateMachine.InitializeState(_attackState,
            onExecute: ExecuteAttack);
        
        StateMachine.InitializeState(_specialAttackState,
            onExecute: ExecuteSpecialAttack,
            onEnter: EnterSpecialAttack);
    }

    /// <summary>
    /// Логика особой атаки
    /// </summary>
    private void ExecuteSpecialAttack()
    {
        SpecialAttackHandler();
    }
    /// <summary>
    /// Логика атаки
    /// </summary>
    private void ExecuteAttack()
    {
        AttackHandler();
    }
    /// <summary>
    /// Обычная логика
    /// </summary>
    private void ExecuteDefault()
    {
        SetDirectionTo();
    }

    /// <summary>
    /// При начале особой атаки
    /// </summary>
    private void EnterSpecialAttack()
    {
        SetDirectionTo();
        _canSpecialAttack = false;
    }

    /// <summary>
    /// Событие на проверку здоровья босса
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void CheckBossHP(NumericAttrib hp)
    {
        _isBossHalfHP = hp.Value <= hp.MaxValue / 2;

        if (_isBossHalfHP && _canSpecialAttack)
            InputSpecialAttack();

        if (hp.Value <= hp.MaxValue / 4 && StateMachine.IsCurrent(_specialAttackState))
            StateMachine.EnterTo(_defaultState);
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
        _canSpecialAttack = true;
    }
}
