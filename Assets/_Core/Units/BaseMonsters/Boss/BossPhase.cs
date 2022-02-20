using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

/// <summary>
/// Объект фазы босса
/// </summary>
[CreateAssetMenu(fileName = "NewBossPhase", menuName = "Boss Phases/Create New")]
public class BossPhase : ScriptableObject
{
    [Header("Basic Information")]
    [SerializeField] protected string _phaseName;
    public string PhaseName { get => _phaseName; private set => _phaseName = value; }

    [Header("Skin For Phase")]
    [SerializeField] protected SpriteLibraryAsset _skinAsset;

    //===>> Components & Fields <<===\\

    private List<BossPhaseScript> _phasesScripts = new List<BossPhaseScript>();

    /// <summary>
    /// Событие при смене фазы
    /// </summary>
    public event Action<BossPhase> OnPhaseChanged;

    //===>> Public Methods <<===\\

    /// <returns>
    /// Скин фазы
    /// </returns>
    public SpriteLibraryAsset GetSkinAsset() => _skinAsset;

    /// <summary>
    /// Выполняет скрипт логики фазы босса
    /// </summary>
    /// <returns>
    /// Фаза босса
    /// </returns>
    /// <param name="scriptLabel">Лэйбл скрипта</param>
    public BossPhase InvokeScript(string scriptLabel) => GetScript(scriptLabel)?.Invoke(this);

    /// <returns>
    /// Скрипт фазы босса
    /// </returns>
    /// <param name="scriptLabel">Лэйбл скрипта</param>
    public BossPhaseScript GetScript(string scriptLabel) => _phasesScripts.Find(s => s.Label == scriptLabel);

    /// <summary>
    /// Добавляет ноовый скрипт поведения
    /// </summary>
    /// <param name="scriptLabel">Лэйбл скрипта</param>
    /// <param name="script">Действие скрипта</param>
    public void CreateScript(string scriptLabel, Action script)
    {
        if (!_phasesScripts.Exists(s => s.Label == scriptLabel))
            _phasesScripts.Add(new BossPhaseScript(scriptLabel, script));
        else
            Debug.LogError("Зафиксирована попытка добавить уже существующий скрипт для босса.");
    }

    /// <summary>
    /// Изменяет фазу босса
    /// </summary>
    /// <param name="skinner">Компонент меняющий скин сущности</param>
    public void ChangePhase(SkinChanger skinner)
    {
        skinner.ChangeFullSkin(_skinAsset);
        OnPhaseChanged?.Invoke(this);
    }

    //===>> System <<===\\

    public override bool Equals(object obj)
    {
        return obj is BossPhase phase &&
               base.Equals(obj) &&
               PhaseName == phase.PhaseName;
    }
    public override int GetHashCode()
    {
        int hashCode = -851507791;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhaseName);
        return hashCode;
    }
    public static bool operator ==(BossPhase a, BossPhase b) => a.PhaseName == b.PhaseName;
    public static bool operator !=(BossPhase a, BossPhase b) => a.PhaseName != b.PhaseName;
}