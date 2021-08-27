using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonSpawner", menuName = "Custom/DungeonGeneration/DungeonSpawnerParameters")]
public class DungeonSpawnerParameters : ScriptableObject
{
    public eDungeonSpawnerType spawnerType;
    public Vector2Int spawnPosition;
    public GameObject spawnTarget;
}

public enum eDungeonSpawnerType
{
    PlayerSpawner,
}