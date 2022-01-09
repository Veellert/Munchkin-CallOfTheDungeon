using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ���������� � ������ ������� 1 ����� 1 ������
/// </summary>
[RequireComponent(typeof(SkinChanger))]
public class SmileyFaceBossMinion : Monster
{
    /// <summary>
    /// ����� �������
    /// </summary>
    /// <param name="spawnPoint">����� ������</param>
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
    /// ������ �������
    /// </summary>
    public override void Die()
    {
        base.Die();

        SmileyFaceBoss.minionCount--;
    }

    /// <summary>
    /// ������� ������������� ������
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
