using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSingleMotionMonsterDungeonSpawner", menuName = "Custom/DungeonGeneration/Spawner/SingleMotionMonsterSpawner")]
public class SingleMotionMonsterDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType => eDungeonSpawnerType.SingleMotionMonsterSpawner;
    public MotionMonster monsterTarget;
    private MotionMonster _currentTarget;

    public override void Spawn()
    {
        _currentTarget = Instantiate(monsterTarget, (Vector3Int)spawnPosition, Quaternion.identity);
    }

    public override void Destroy()
    {
        DestroyImmediate(_currentTarget);
    }
}