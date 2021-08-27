using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerDungeonSpawner", menuName = "Custom/DungeonSpawners/PlayerSpawner")]
public class PlayerDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType { get => eDungeonSpawnerType.PlayerSpawner; }
    public GameObject playerTarget;

    public override void Spawn()
    {
        Instantiate(playerTarget, (Vector3Int)spawnPosition, Quaternion.identity).name = "Player";
    }
}