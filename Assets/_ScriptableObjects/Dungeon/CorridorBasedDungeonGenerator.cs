using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCorridorDungeon", menuName = "Custom/DungeonGeneration/Dungeon/CorridorBased")]
public class CorridorBasedDungeonGenerator : DungeonParameters
{
    public int _corridorLength = 40;
    public int _corridorsCount = 5;
    [Range(0.1f, 1)] public float _roomPercent = 0.8f;
    public DungeonRoomParameters _dungeonRoomParameters;

    protected override void Generate(Vector2Int startPosition)
    {
        var floorPositions = new HashSet<Vector2Int>();
        var potentialRoomsPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomsPositions, startPosition);

        var preparedRoomsPositions = GetPreparedRoomsPositions(potentialRoomsPositions);
        var deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnds(deadEnds, preparedRoomsPositions);

        floorPositions.UnionWith(preparedRoomsPositions);
        _preparedPositions = floorPositions;
    }

    protected override void Clear()
    {
        _dungeonRoomParameters.DestroyDungeonSpawners();
    }

    protected override void Visualize()
    {
        _dungeonRoomParameters.InitDungeonSpawners(_preparedPositions);
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

    private HashSet<Vector2Int> GetPreparedRoomsPositions(HashSet<Vector2Int> roomsPositions)
    {
        var roomPositions = new HashSet<Vector2Int>();
        int preparedRoomsCount = Mathf.RoundToInt(roomsPositions.Count * _roomPercent);

        var preparedRooms = roomsPositions.OrderBy(s => Guid.NewGuid()).Take(preparedRoomsCount).ToList();

        foreach (var roomPosition in preparedRooms)
        {
            var roomFloor = DungeonRoomGenerator.CreateFloor(_dungeonRoomParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomsPositions)
    {
        foreach (var position in deadEnds)
        {
            if (!roomsPositions.Contains(position))
            {
                var room = DungeonRoomGenerator.CreateFloor(_dungeonRoomParameters, position);
                roomsPositions.UnionWith(room);
            }
        }
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
