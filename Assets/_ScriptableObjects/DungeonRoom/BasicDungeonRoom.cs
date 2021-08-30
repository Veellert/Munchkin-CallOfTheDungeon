using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBasicDungeonRoom", menuName = "Custom/DungeonGeneration/Room/BasicRoom")]
public class BasicDungeonRoom : DungeonRoomParameters
{
    public override eDungeonRoomType RoomType => eDungeonRoomType.BasicRoom;

    public override void DestroyDungeonSpawners()
    {
        
    }

    public override void InitDungeonSpawners(HashSet<Vector2Int> floorPositions)
    {
        
    }
}
