using UnityEngine;

/// <summary>
/// Компонент родитель миньона босса
/// </summary>
public abstract class MinionBoss<TBoss> : AgressiveMonster
    where TBoss : Boss
{
    //===>> Components & Fields <<===\\

    protected TBoss _boss;

    //===>> Important Methods <<===\\

    /// <returns>
    /// Созданный миньон
    /// </returns>
    /// <param name="spawnPoint">Точка спавна</param>
    /// <param name="minion">Миньон</param>
    /// <param name="boss">Босс</param>
    public virtual MinionBoss<TBoss> Spawn(Vector2 spawnPoint, MinionBoss<TBoss> minion, TBoss boss)
    {
        var monster = Instantiate(minion, spawnPoint, Quaternion.identity);
        monster._boss = boss;

        return monster;
    }
}