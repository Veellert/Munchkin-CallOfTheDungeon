using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStartDungeonRoom", menuName = "Custom/DungeonGeneration/Room/StartRoom")]
public class StartDungeonRoom : DungeonRoomParameters
{
    public PlayerDungeonSpawner playerSpawner;

    public override eDungeonRoomType RoomType => eDungeonRoomType.StartRoom;

    public override void DestroyDungeonSpawners()
    {
        playerSpawner.Destroy();
    }

    public override void InitDungeonSpawners(HashSet<Vector2Int> spawnPositions)
    {
        playerSpawner.SetSpawnPosition(spawnPositions.ElementAt(Random.Range(0, spawnPositions.Count)));
        var allSpawners = new DungeonSpawnerParameters[1] { playerSpawner };
        DungeonSpawnersInitializer.InitSpawners(allSpawners);
    }
}
