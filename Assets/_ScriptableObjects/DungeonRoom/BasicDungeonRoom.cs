using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBasicDungeonRoom", menuName = "Custom/DungeonGeneration/Room/BasicRoom")]
public class BasicDungeonRoom : DungeonRoomParameters
{
    public override eDungeonRoomType RoomType => eDungeonRoomType.BasicRoom;
    public List<GroupMonsterDungeonSpawner> monsterSpawners;

    public override void DestroyDungeonSpawners()
    {
        foreach (var spawner in monsterSpawners)
            spawner?.Destroy();
    }

    public override void InitDungeonSpawners(HashSet<Vector2> spawnPositions)
    {
        foreach (var spawner in monsterSpawners)
            spawner.SetSpawnPosition(spawnPositions.ElementAt(Random.Range(0, spawnPositions.Count)));

        DungeonSpawnersInitializer.InitSpawners(monsterSpawners.ToArray());
    }
}
