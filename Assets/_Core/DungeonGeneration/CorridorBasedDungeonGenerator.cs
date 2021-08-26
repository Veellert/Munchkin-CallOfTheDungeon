using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorBasedDungeonGenerator : DungeonGeneratorTemplate
{
    [SerializeField] private int _corridorLength = 40;
    [SerializeField] private int _corridorsCount = 5;
    [SerializeField] [Range(0.1f, 1)] private float _roomPercent = 0.8f;

    protected override void StartGeneration()
    {
        var floorPositions = new HashSet<Vector2Int>();
        var potentialRoomsPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomsPositions);

        var preparedRoomsPositions = GetPreparedRoomsPositions(potentialRoomsPositions);
        var deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnds(deadEnds, preparedRoomsPositions);

        floorPositions.UnionWith(preparedRoomsPositions);
        _currentVisualizePositions = floorPositions;

        VisualizeDungeon();
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

            if (nearCount == 1)
                deadEnds.Add(position);
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
            var roomFloor = GetParametersBasedPositions(_dungeonRoomParameters, roomPosition);
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
                var room = GetParametersBasedPositions(_dungeonRoomParameters, position);
                roomsPositions.UnionWith(room);
            }
        }
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomsPositions)
    {
        var currentPosition = _startPosition;
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
