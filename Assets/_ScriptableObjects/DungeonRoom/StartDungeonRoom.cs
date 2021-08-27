using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStartDungeonRoom", menuName = "Custom/DungeonGeneration/StartDungeonRoom")]
public class StartDungeonRoom : DungeonRoomParameters
{
    public PlayerDungeonSpawner playerSpawner;
    private bool _isSpawnersInited;

    public override eDungeonRoomType RoomType => eDungeonRoomType.StartRoom;

    public override void DestroyDungeonSpawners()
    {
        playerSpawner.Destroy();
        _isSpawnersInited = false;
    }

    public override void InitDungeonSpawners(HashSet<Vector2Int> floorPositions)
    {
        if(!_isSpawnersInited)
        {
            playerSpawner.SetSpawnPosition(floorPositions.ElementAt(Random.Range(0, floorPositions.Count)));
            var allSpawners = new DungeonSpawnerParameters[1] { playerSpawner };
            DungeonSpawnersInitializer.InitSpawners(allSpawners);
            _isSpawnersInited = true;
        }
    }

}
