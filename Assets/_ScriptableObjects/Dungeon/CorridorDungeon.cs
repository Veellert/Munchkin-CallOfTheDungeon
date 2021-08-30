using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCorridorDungeon", menuName = "Custom/DungeonGeneration/Dungeon/CorridorBased")]
public class CorridorDungeon : DungeonParameters
{
    public int _corridorLength = 40;
    public int _corridorsCount = 5;
    [Range(0.1f, 1)] public float _roomPercent = 0.8f;

    public StartDungeonRoom _startDungeonRoom;
    [SerializeField] [HideInInspector] private HashSet<Vector2Int> _startRoomPreparedPositions = new HashSet<Vector2Int>();

    public BasicDungeonRoom _basicDungeonRoom;
    [SerializeField] [HideInInspector] private HashSet<Vector2Int> _basicRoomPreparedPositions = new HashSet<Vector2Int>();

    protected override void Generate(Vector2Int startPosition)
    {
        var floorPositions = new HashSet<Vector2Int>();
        var potentialRoomsPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomsPositions, startPosition);

        var preparedRoomsPositions = CreatePreparedRooms(potentialRoomsPositions);
        var deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnds(deadEnds, preparedRoomsPositions);

        floorPositions.UnionWith(preparedRoomsPositions);
        _preparedPositions = floorPositions;
    }

    protected override void Clear()
    {
        _startDungeonRoom.DestroyDungeonSpawners();
        _basicDungeonRoom.DestroyDungeonSpawners();
    }

    protected override void Visualize()
    {
        _startDungeonRoom.InitDungeonSpawners(_startRoomPreparedPositions);
        _basicDungeonRoom.InitDungeonSpawners(_basicRoomPreparedPositions);
    }

    private HashSet<Vector2Int> CreatePreparedRooms(HashSet<Vector2Int> roomsPositions)
    {
        var roomPositions = new HashSet<Vector2Int>();
        _startRoomPreparedPositions.Clear();
        _basicRoomPreparedPositions.Clear();
        int preparedRoomsCount = Mathf.RoundToInt(roomsPositions.Count * _roomPercent);

        var preparedRooms = roomsPositions.OrderBy(s => Guid.NewGuid()).Take(preparedRoomsCount).ToList();

        int index = 0;
        foreach (var roomPosition in preparedRooms)
        {
            var roomFloor = DungeonRoomGenerator.CreateFloor(_basicDungeonRoom, roomPosition);
            if (index == 0)
                roomFloor = DungeonRoomGenerator.CreateFloor(_startDungeonRoom, roomPosition);

            if (index == 0)
                _startRoomPreparedPositions.UnionWith(roomFloor);
            else
                _basicRoomPreparedPositions.UnionWith(roomFloor);

            roomPositions.UnionWith(roomFloor);
            index++;
        }

        return roomPositions;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomsPositions)
    {
        foreach (var position in deadEnds)
        {
            if (!roomsPositions.Contains(position))
            {
                var roomFloor = DungeonRoomGenerator.CreateFloor(_basicDungeonRoom, position);
                _basicRoomPreparedPositions.UnionWith(roomFloor);
                roomsPositions.UnionWith(roomFloor);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        var deadEnds = new List<Vector2Int>();

        foreach (var position in floorPositions)
        {
            int nearCount = 0;

            foreach (var direction in Direction2D.StandartDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    nearCount++;
            }

            if (nearCount == 1) deadEnds.Add(position);
        }

        return deadEnds;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomsPositions, Vector2Int startPosition)
    {
        var currentPosition = startPosition;
        potentialRoomsPositions.Add(currentPosition);

        for (int i = 0; i < _corridorsCount; i++)
        {
            var corridorPositions = DungeonGenerationAlgorithms.GetRandomCorridorPositions(currentPosition, _corridorLength);

            currentPosition = corridorPositions[corridorPositions.Count - 1];
            potentialRoomsPositions.Add(currentPosition);
            floorPositions.UnionWith(corridorPositions);
        }
    }
}
