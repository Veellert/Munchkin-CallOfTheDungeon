using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику миньона 1 босса 1 уровня
/// </summary>
[RequireComponent(typeof(SkinChanger))]
public class SmileyFaceMinion : MinionBoss<SmileyFaceBoss>
{
    //===>> Components & Fields <<===\\

    private SkinChanger _skinner;

    //===>> Important Methods <<===\\

    public override MinionBoss<SmileyFaceBoss> Spawn(Vector2 spawnPoint, MinionBoss<SmileyFaceBoss> minion, SmileyFaceBoss boss)
    {
        var monster = (SmileyFaceMinion)base.Spawn(spawnPoint, minion, boss);
        monster._skinner = monster.GetComponent<SkinChanger>();
        monster._skinner.ChangeFullSkin(monster._boss.CurrentBossPhase.GetSkinAsset());

        return monster;
    }

    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        ChaseRadius = int.MaxValue;
    }

    //===>> Interfaces Methods <<===\\

    public override void Die()
    {
        base.Die();

        _boss.MinionCount.DecreaseValue();
    }
}
