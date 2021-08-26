using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vector = UnityEngine.Vector2Int;
using vectorhash = System.Collections.Generic.HashSet<UnityEngine.Vector2Int>;

public class RoomBasedDungeonGenerator : DungeonGeneratorTemplate
{
    [SerializeField] private int _minRoomWidth = 4;
    [SerializeField] private int _minRoomHeight = 4;

    [SerializeField] private int _dungeonWidth = 20;
    [SerializeField] private int _dungeonHeight = 20;

    [SerializeField] [Range(0, 10)] private int _offset = 1;

    [SerializeField] private bool _isRandomShapeRooms = false;

    protected override void StartGeneration()
    {
        var roomsList = DungeonGenerationAlgorithms.GetSpacePartitioning(
            new BoundsInt((Vector3Int)_startPosition, new Vector3Int(_dungeonWidth, _dungeonHeight, 0)),
            _minRoomWidth, _minRoomHeight);

        var roomsPositions = new vectorhash();

        if (_isRandomShapeRooms)
            roomsPositions = CreateRandomShapeRooms(roomsList);
        else
            roomsPositions = CreateSimpleRooms(roomsList);

        var roomsCenters = new List<vector>();
        foreach (var room in roomsList)
            roomsCenters.Add((vector)Vector3Int.RoundToInt(room.center));

        var corridors = ConnectRooms(roomsCenters);
        roomsPositions.UnionWith(corridors);
        _currentVisualizePositions = roomsPositions;

        VisualizeDungeon();
    }
    
    private vectorhash CreateRandomShapeRooms(List<BoundsInt> roomsList)
    {
        var floorPositions = new vectorhash();

        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomsBounds = roomsList[i];
            var roomCenter = new vector(Mathf.RoundToInt(roomsBounds.center.x), Mathf.RoundToInt(roomsBounds.center.y));

            var roomFloorPositions = GetParametersBasedPositions(_dungeonRoomParameters, roomCenter);

            foreach (var position in roomFloorPositions)
            {
                if(position.x >= (roomsBounds.xMin + _offset) && position.x <= (roomsBounds.xMax - _offset) &&
                    position.y >= (roomsBounds.yMin + _offset) && position.y <= (roomsBounds.yMax - _offset))
                {
                    floorPositions.Add(position);
                }
            }
        }

        return floorPositions;
    }

    private vectorhash ConnectRooms(List<vector> roomCenters)
    {
        var corridorsPositions = new vectorhash();
        var currnetRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];

        roomCenters.Remove(currnetRoomCenter);

        while (roomCenters.Count > 0)
        {
            var closestRoom = FindClosestPointTo(currnetRoomCenter, roomCenters);
            roomCenters.Remove(closestRoom);
            var corridorPositions = CreateCorridor(currnetRoomCenter, closestRoom);
            currnetRoomCenter = closestRoom;
            corridorsPositions.UnionWith(corridorPositions);
        }

        return corridorsPositions;
    }

    private vectorhash CreateCorridor(vector currnetRoomCenter, vector destination)
    {
        var corridorPositions = new vectorhash();

        var position = currnetRoomCenter;
        corridorPositions.Add(position);

        while (position.y != destination.y)
        {
            if(destination.y > position.y)
                position += vector.up;
            else if (destination.y < position.y)
                position += vector.down;

            corridorPositions.Add(position);
        }

        while (position.x != destination.x)
        {
            if(destination.x > position.x)
                position += vector.right;
            else if (destination.x < position.x)
                position += vector.left;

            corridorPositions.Add(position);
        }

        return corridorPositions;
    }

    private vector FindClosestPointTo(vector currnetRoomCenter, List<vector> roomCenters)
    {
        var closestPoint = vector.zero;
        float distance = float.MaxValue;

        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currnetRoomCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closestPoint = position;
            }
        }
        return closestPoint;
    }

    private vectorhash CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        var floorPositions = new vectorhash();

        foreach (var room in roomsList)
            for (int col = _offset; col < room.size.x - _offset; col++)
                for (int row = _offset; row < room.size.y - _offset; row++)
                {
                    var position = (vector)room.min + new vector(col, row);
                    floorPositions.Add(position);
                }

        return floorPositions;
    }
}
