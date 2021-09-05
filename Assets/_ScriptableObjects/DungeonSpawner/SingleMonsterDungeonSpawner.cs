using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSingleMonsterDungeonSpawner", menuName = "Custom/DungeonGeneration/Spawner/SingleMonsterSpawner")]
public class SingleMonsterDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType => eDungeonSpawnerType.SingleMonsterSpawner;
    public MonsterParameters monsterTarget;
    private GameObject currentTarget;

    public override void Spawn()
    {
        monsterTarget.Instantiate((Vector3Int)spawnPosition);
    }
    public override void Destroy()
    {
        DestroyImmediate(currentTarget);
    }
}