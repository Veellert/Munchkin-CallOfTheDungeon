using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSectionDungeon", menuName = "Custom/DungeonGeneration/Dungeon/SectionBased")]
public class SectionDungeon : DungeonParameters
{
    public int _minRoomWidth = 4;
    public int _minRoomHeight = 4;

    public int _dungeonWidth = 20;
    public int _dungeonHeight = 20;

    public bool _isRandomShapeRooms = false;
    [Range(0, 10)] public int _offset = 1;

    public StartDungeonRoom _startDungeonRoom;
    [SerializeField] [HideInInspector] private HashSet<Vector2Int> _startRoomPreparedPositions = new HashSet<Vector2Int>();

    public BasicDungeonRoom _basicDungeonRoom;
    [SerializeField] [HideInInspector] private HashSet<Vector2Int> _basicRoomPreparedPositions = new HashSet<Vector2Int>();

    protected override void Generate(Vector2Int startPosition)
    {
        var roomsList = DungeonGenerationAlgorithms.GetSpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(_dungeonWidth, _dungeonHeight, 0)),
            _minRoomWidth, _minRoomHeight);

        var roomsPositions = CreateSimpleRooms(roomsList);

        if (_isRandomShapeRooms)
            roomsPositions = CreateRandomShapeRooms(roomsList);

        var roomsCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
            roomsCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));

        var corridors = ConnectRooms(roomsCenters);
        roomsPositions.UnionWith(corridors);
        _preparedPositions = roomsPositions;
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

    private HashSet<Vector2Int> CreateRandomShapeRooms(List<BoundsInt> roomsList)
    {
        var floorPositions = new HashSet<Vector2Int>();
        _startRoomPreparedPositions.Clear();
        _basicRoomPreparedPositions.Clear();

        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomsBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomsBounds.center.x), Mathf.RoundToInt(roomsBounds.center.y));

            var roomFloorPositions = DungeonRoomGenerator.CreateFloor(_basicDungeonRoom, roomCenter);
            if(i == 0)
            roomFloorPositions = DungeonRoomGenerator.CreateFloor(_startDungeonRoom, roomCenter);

            foreach (var position in roomFloorPositions)
            {
                if(position.x >= (roomsBounds.xMin + _offset) && position.x <= (roomsBounds.xMax - _offset) &&
                    position.y >= (roomsBounds.yMin + _offset) && position.y <= (roomsBounds.yMax - _offset))
                {
                    floorPositions.Add(position);
                }
            }

            if (i == 0)
                _startRoomPreparedPositions.UnionWith(floorPositions);
            else
                _basicRoomPreparedPositions.UnionWith(floorPositions);
        }

        return floorPositions;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        var floorPositions = new HashSet<Vector2Int>();
        _startRoomPreparedPositions.Clear();
        _basicRoomPreparedPositions.Clear();

        int index = 0;
        foreach (var room in roomsList)
        {
            for (int col = _offset; col < room.size.x - _offset; col++)
                for (int row = _offset; row < room.size.y - _offset; row++)
                {
                    var position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floorPositions.Add(position);
                }

            if (index == 0)
                _startRoomPreparedPositions.UnionWith(floorPositions);
            else
                _basicRoomPreparedPositions.UnionWith(floorPositions);

            index++;
        }

        return floorPositions;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        var corridorsPositions = new HashSet<Vector2Int>();
        var currnetRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];

        roomCenters.Remove(currnetRoomCenter);

        while (roomCenters.Count > 0)
        {
            var closestRoom = FindClosestPoint(currnetRoomCenter, roomCenters);
            roomCenters.Remove(closestRoom);
            var corridorPositions = CreateCorridor(currnetRoomCenter, closestRoom);
            currnetRoomCenter = closestRoom;
            corridorsPositions.UnionWith(corridorPositions);
        }

        return corridorsPositions;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currnetRoomCenter, Vector2Int destination)
    {
        var corridorPositions = new HashSet<Vector2Int>();

        var position = currnetRoomCenter;
        corridorPositions.Add(position);

        while (position.y != destination.y)
        {
            if(destination.y > position.y)
                position += Vector2Int.up;
            else if (destination.y < position.y)
                position += Vector2Int.down;

            corridorPositions.Add(position);
        }

        while (position.x != destination.x)
        {
            if(destination.x > position.x)
                position += Vector2Int.right;
            else if (destination.x < position.x)
                position += Vector2Int.left;

            corridorPositions.Add(position);
        }

        return corridorPositions;
    }

    private Vector2Int FindClosestPoint(Vector2Int currnetRoomCenter, List<Vector2Int> roomCenters)
    {
        var closestPoint = Vector2Int.zero;
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
}
