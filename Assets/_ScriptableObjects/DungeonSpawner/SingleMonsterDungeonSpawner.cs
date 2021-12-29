using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSingleMonsterSpawner", menuName = "Custom/DungeonGeneration/Spawner/SingleMonsterSpawner")]
public class SingleMonsterDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType => eDungeonSpawnerType.SingleMonsterSpawner;
    public Monster monsterTarget;
    private Monster _currentTarget;

    public override void Spawn()
    {
        _currentTarget = Instantiate(monsterTarget, (Vector3)spawnPosition, Quaternion.identity);
    }

    public override void Destroy()
    {
        DestroyImmediate(_currentTarget);
    }
}