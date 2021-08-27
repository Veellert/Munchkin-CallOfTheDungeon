using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerDungeonSpawner", menuName = "Custom/DungeonSpawners/PlayerDungeonSpawner")]
public class PlayerDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType => eDungeonSpawnerType.PlayerSpawner;
    public GameObject playerTarget;
    private GameObject currentTarget;

    public override void Spawn()
    {
        currentTarget = Instantiate(playerTarget, (Vector3Int)spawnPosition, Quaternion.identity);
        currentTarget.name = "Player";
    }
    public override void Destroy()
    {
        DestroyImmediate(currentTarget);
    }
}