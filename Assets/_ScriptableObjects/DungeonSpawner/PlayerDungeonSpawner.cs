using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSpawner", menuName = "Custom/DungeonGeneration/Spawner/PlayerSpawner")]
public class PlayerDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType => eDungeonSpawnerType.PlayerSpawner;
    public Player playerTarget;
    private Player _currentTarget;

    public override void Spawn()
    {
        _currentTarget = Instantiate(playerTarget, (Vector3)spawnPosition, Quaternion.identity);
    }

    public override void Destroy()
    {
        DestroyImmediate(_currentTarget);
    }
}