using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику миньона 1 босса 1 уровня
/// </summary>
[RequireComponent(typeof(SkinChanger))]
public class SmileyFaceMinion : MinionBoss<SmileyFaceBoss>
{
    private SkinChanger _skinner;

    public override MinionBoss<SmileyFaceBoss> Spawn(Vector2 spawnPoint, MinionBoss<SmileyFaceBoss> minion, SmileyFaceBoss boss)
    {
        var monster = (SmileyFaceMinion)base.Spawn(spawnPoint, minion, boss);
        monster._skinner = monster.GetComponent<SkinChanger>();
        monster._skinner.ChangeFullSkin(monster._boss.GetCurrentPhase().GetSkinAsset());

        return monster;
    }

    public override void Die()
    {
        base.Die();

        SmileyFaceBoss._minionCount--;
    }

    protected override void ChaseInput() => StateMachine.EnterTo(_chaseState);

    protected override void InitializeStates()
    {
        base.InitializeStates();

        StateMachine.InitializeState(_defaultState, onExecute: OnExecuteDefault);
        StateMachine.InitializeState(_attackState, onExecute: OnExecuteAttack);
    }

    /// <summary>
    /// Логика атаки
    /// </summary>
    private void OnExecuteAttack()
    {
        AttackHandler();
    }
    /// <summary>
    /// Обычная логика
    /// </summary>
    private void OnExecuteDefault()
    {
        SetDirectionTo();
    }
}
