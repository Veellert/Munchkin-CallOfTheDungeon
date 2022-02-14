using UnityEngine;

/// <summary>
/// Компонент родитель миньона босса
/// </summary>
public abstract class MinionBoss<T> : Monster where T : Boss
{
    protected T _boss;

    /// <returns>
    /// Созданный миньон
    /// </returns>
    /// <param name="spawnPoint">Точка спавна</param>
    /// <param name="minion">Миньон</param>
    /// <param name="boss">Босс</param>
    public virtual MinionBoss<T> Spawn(Vector2 spawnPoint, MinionBoss<T> minion, T boss)
    {
        var monster = Instantiate(minion, spawnPoint, Quaternion.identity);
        monster._boss = boss;

        return monster;
    }
}