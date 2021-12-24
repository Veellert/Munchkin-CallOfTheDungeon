using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSingleMonsterDungeonSpawner", menuName = "Custom/DungeonGeneration/Spawner/SingleMonsterSpawner")]
public class SingleMonsterDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType => eDungeonSpawnerType.SingleMotionMonsterSpawner;
    public Monster monsterTarget;
    private Monster _currentTarget;

    public override void Spawn()
    {
        _currentTarget = Instantiate(monsterTarget, (Vector3Int)spawnPosition, Quaternion.identity);
    }

    public override void Destroy()
    {
        DestroyImmediate(_currentTarget);
    }
}