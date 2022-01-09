using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий а логику миниона 1 босса 1 уровня
/// </summary>
[RequireComponent(typeof(SkinChanger))]
public class SmileyFaceBossMinion : Monster
{
    /// <summary>
    /// Спавн миниона
    /// </summary>
    /// <param name="spawnPoint">Точка спавна</param>
    public static void Spawn(Vector2 spawnPoint, SmileyFaceBossMinion minion, SmileyFaceBoss boss)
    {
        var monster = Instantiate(minion, spawnPoint, Quaternion.identity);
        monster._boss = boss;
        monster._skinner = monster.GetComponent<SkinChanger>();
        monster._skinner.ChangeFullSkin(monster._boss.CurrentBossPhase);
    }

    private SmileyFaceBoss _boss;
    private SkinChanger _skinner;

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
                AttackHandler(new TileHalf());
                break;
        }
    }

    /// <summary>
    /// Смерть миниона
    /// </summary>
    public override void Die()
    {
        base.Die();

        SmileyFaceBoss.minionCount--;
    }

    /// <summary>
    /// Попытка преследования игрока
    /// </summary>
    protected override void TryChase()
    {
        if (_chaseTarget == null)
            _chaseTarget = Player.Instance.transform;
        var target = _chaseTarget.GetComponent<IDamageable>();

        if (!target.IsDead)
        {
            SetDirection(_chaseTarget.position);
            if (_state != eState.Chase)
                _state = eState.Chase;
        }
    }
}
