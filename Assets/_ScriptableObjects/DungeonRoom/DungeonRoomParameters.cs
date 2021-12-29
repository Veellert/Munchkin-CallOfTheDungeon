using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonRoomParameters : ScriptableObject
{
    public int iterations = 10;
    public int floorPositionsCount = 10;
    public bool isRandomEachIteration = true;

    public abstract eDungeonRoomType RoomType { get; }
    public abstract void InitDungeonSpawners(HashSet<Vector2> _currentVisualizePositions);
    public abstract void DestroyDungeonSpawners();
}

public enum eDungeonRoomType
{
    StartRoom,
    BasicRoom,
}
