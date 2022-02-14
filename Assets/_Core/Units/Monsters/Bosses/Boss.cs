using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику босса
/// </summary>
[RequireComponent(typeof(SkinChanger))]
public abstract class Boss : Monster
{
    [Header("Default Boss Phase")]
    [SerializeField] protected BossPhase _default;
    
    protected SkinChanger _skinner;

    protected List<BossPhase> _bossPhases = new List<BossPhase>();
    protected BossPhase _currentBossPhase;

    protected override void Start()
    {
        base.Start();
        InitBossPhases();
        InitPhasesScripts();
        ChangeBossPhaseTo(_default);
    }

    /// <returns>
    /// Текущая фаза босса
    /// </returns>
    public BossPhase GetCurrentPhase() => _currentBossPhase;

    /// <summary>
    /// Инициализаця скриптов для фаз босса
    /// </summary>
    protected abstract void InitPhasesScripts();

    protected override void GetRequiredComponents()
    {
        base.GetRequiredComponents();

        _skinner = GetComponent<SkinChanger>();
    }

    /// <summary>
    /// Инициализаця фаз босса
    /// </summary>
    protected virtual void InitBossPhases()
    {
        AddBossPhase(_default);
    }

    /// <summary>
    /// Добавляет фазу для босса
    /// </summary>
    /// <param name="bossPhase">Фаза босса</param>
    protected void AddBossPhase(BossPhase bossPhase) => _bossPhases.Add(bossPhase);

    /// <summary>
    /// Изеняет фазу босса на нужную
    /// </summary>
    /// <param name="bossPhase">Фаза босса</param>
    protected void ChangeBossPhaseTo(BossPhase bossPhase)
    {
        if (_currentBossPhase && _currentBossPhase.PhaseName == bossPhase.PhaseName)
            return;

        _currentBossPhase = _bossPhases.Find(s => s.PhaseName == bossPhase.PhaseName);
        _currentBossPhase.ChangePhase(_skinner);
    }

    /// <summary>
    /// Создает скрипт поведения для фазы
    /// </summary>
    /// <param name="scriptLabel">Лэйбл скрипта</param>
    /// <param name="defaultAction">Действие по умолчанию для неназначенных фаз</param>
    /// <param name="phasesActions">Список с фазами босса и действиями для каждй из них</param>
    protected void CreatePhasesScript(string scriptLabel, Action defaultAction, List<(BossPhase phase, Action scriptAction)> phasesActions)
    {
        foreach (var phase in _bossPhases)
            SetDefaultScriptIfNotImplemented(phase);

        phasesActions.ForEach(phase => SetScript(phase.phase.PhaseName, phase.scriptAction));

        void SetScript(string phaseName, Action script)
            => _bossPhases.Find(s => s.PhaseName == phaseName)?.CreateScript(scriptLabel, script);

        void SetDefaultScriptIfNotImplemented(BossPhase phase)
        {
            if (!phasesActions.Exists(s => s.phase.PhaseName == phase.PhaseName))
                phasesActions.Add((phase, defaultAction));
        }
    }
}