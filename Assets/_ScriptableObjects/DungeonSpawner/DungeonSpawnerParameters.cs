using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonSpawnerParameters : ScriptableObject
{
    public abstract eDungeonSpawnerType SpawnerType { get; }
    public Vector2Int spawnPosition;

    public abstract void Spawn();
    public abstract void Destroy();

    public void SetSpawnPosition(int x, int y) => spawnPosition = new Vector2Int(x, y);
    public void SetSpawnPosition(Vector2Int position) => spawnPosition = position;
}

public enum eDungeonSpawnerType
{
    PlayerSpawner,
}
