using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGroupMonsterSpawner", menuName = "Custom/DungeonGeneration/Spawner/GroupMonsterSpawner")]
public class GroupMonsterDungeonSpawner : DungeonSpawnerParameters
{
    public override eDungeonSpawnerType SpawnerType => eDungeonSpawnerType.GroupMonsterSpawner;
    public List<SpawnMonsterInfo> monsterTargetList;
    private List<Monster> _currentTargetList;

    public override void Spawn()
    {
        _currentTargetList = new List<Monster>();

        foreach (var monster in monsterTargetList)
        {
            for (int i = 0; i < monster.monstersCount; i++)
                _currentTargetList.Add(Instantiate(monster.monsterTarget, (Vector3)spawnPosition, Quaternion.identity));
        }
    }

    public override void Destroy()
    {
        foreach (var target in _currentTargetList)
            DestroyImmediate(target);
    }
}

[Serializable]
public class SpawnMonsterInfo
{
    public Monster monsterTarget;
    public int monstersCount;
}
