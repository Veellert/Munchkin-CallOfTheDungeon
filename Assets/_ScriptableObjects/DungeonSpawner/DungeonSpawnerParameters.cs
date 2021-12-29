using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonSpawnerParameters : ScriptableObject
{
    public abstract eDungeonSpawnerType SpawnerType { get; }
    public Vector2 spawnPosition;

    public abstract void Spawn();
    public abstract void Destroy();

    public void SetSpawnPosition(int x, int y) => spawnPosition = new Vector2(x, y);
    public void SetSpawnPosition(Vector2 position) => spawnPosition = position;
}

public enum eDungeonSpawnerType
{
    PlayerSpawner,
    SingleMonsterSpawner,
    GroupMonsterSpawner,
}
