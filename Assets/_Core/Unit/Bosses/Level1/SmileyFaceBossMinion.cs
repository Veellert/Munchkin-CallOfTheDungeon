using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ���������� � ������ ������� 1 ����� 1 ������
/// </summary>
[RequireComponent(typeof(SkinChanger))]
public class SmileyFaceBossMinion : Monster
{
    private SmileyFaceBoss _boss;
    private SkinChanger _skinner;

    /// <summary>
    /// ����� �������
    /// </summary>
    /// <param name="spawnPoint">����� ������</param>
    public void Spawn(Vector2 spawnPoint)
    {
        Instantiate(this, spawnPoint, Quaternion.identity);
    }

    protected override void Start()
    {
        base.Start();

        _skinner = GetComponent<SkinChanger>();
        _boss = (SmileyFaceBoss)GetBoss(typeof(SmileyFaceBoss));
        _skinner.ChangeFullSkin(_boss.CurrentBossPhase);
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
                AttackHandler(new TileHalf());
                break;
        }
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
