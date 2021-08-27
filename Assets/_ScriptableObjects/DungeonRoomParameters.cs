using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonRoom", menuName = "Custom/DungeonGeneration/DungeonRoomParameters")]
public class DungeonRoomParameters : ScriptableObject
{
    [Header("Визуализация комнаты")]
    public int iterations = 10;
    public int floorPositionsCount = 10;
    public bool isRandomEachIteration = true;
    [Space]
    public eDungeonRoomType roomType;
    public DungeonSpawnerParameters[] spawners;
}

public enum eDungeonRoomType
{
    StartRoom,
    BasicRoom,
}
