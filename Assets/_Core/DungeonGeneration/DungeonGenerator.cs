using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum eDungeonRoomType
{
    DefaultRoom,
    StartRoom,
    FinishRoom,
}

[System.Serializable]
public class DungeonRoomGeneration
{
    public int iterations = 10;
    public int floorPositionsCount = 10;
    public bool isRandomEachIteration = true;
    public List<DungeonSpawner> spawnerList = new List<DungeonSpawner>();

    public void InitDungeonSpawners(HashSet<Vector2Int> spawnPositions)
    {
        if (spawnerList.Count == 0)
            return;

        foreach (var spawner in spawnerList)
        {
            if (spawner.monsters.Count == 0)
                continue;

            spawner.SpawnerPosition = GetRandomEmptyPosition(spawnPositions);

            foreach (var monster in spawner.monsters)
                monster?.Spawn(spawner.SpawnerPosition);
        }
    }

    public Vector2Int GetRandomEmptyPosition(HashSet<Vector2Int> spawnPositions)
    {
        var randIndex = Random.Range(0, spawnPositions.Count);
        var position = spawnPositions.ElementAt(randIndex);

        while (!Direction2D.ExistEmptySpace(spawnPositions, position, true))
        {
            randIndex = Random.Range(0, spawnPositions.Count);
            position = spawnPositions.ElementAt(randIndex);
        }

        return position;
    }
}

[System.Serializable]
public class DungeonSpawner
{
    public List<SpawnMonsterInfo> monsters = new List<SpawnMonsterInfo>();
    public Vector2Int SpawnerPosition { get; set; }
}

[System.Serializable]
public class SpawnMonsterInfo
{
    public Monster target;
    public int count = 1;

    public void Spawn(Vector2Int spawnerPosition)
    {
        for (int i = 0; i < count; i++)
            GameObject.Instantiate(target, (Vector2)spawnerPosition, Quaternion.identity);
    }
}

public class DungeonRoom
{
    public DungeonRoomGeneration Parameters { get; private set; }
    public HashSet<Vector2Int> FloorPositions { get; set; }

    public eDungeonRoomType RoomType { get; set; }

    public Vector2Int SpawnPosition { get; private set; }
    public bool IsPositionInited { get; private set; }

    public Vector2Int Size
    {
        get
        {
            _max = Vector2Int.zero;
            foreach (var position in FloorPositions)
            {
                if (position.x > _max.x)
                    _max.x = position.x;
                if (position.y > _max.y)
                    _max.y = position.y;
            }

            _min = _max;
            foreach (var position in FloorPositions)
            {
                if (position.x < _min.x)
                    _min.x = position.x;
                if (position.y < _min.y)
                    _min.y = position.y;
            }

            return _max - _min;
        }
    }
    public Vector2Int _max;
    public Vector2Int _min;

    public Vector2Int _tL => new Vector2Int(_min.x, _max.y);
    public Vector2Int _tR => _max;
    public Vector2Int _bL => _min;
    public Vector2Int _bR => new Vector2Int(_max.x, _min.y);

    public List<Vector2Int> _floorPositionList => FloorPositions.ToList();

    public DungeonRoom(DungeonRoomGeneration reference)
    {
        Parameters = reference;
        RoomType = eDungeonRoomType.DefaultRoom;
    }

    public void SetPosition(int x, int y)
    {
        SpawnPosition = new Vector2Int(x, y);
        IsPositionInited = true;
    }
    public void SetPosition(Vector2Int position)
    {
        SpawnPosition = position;
        IsPositionInited = true;
    }

    public bool RoomOverlaped(DungeonRoom room, int offset)
    {
        foreach (var pos in _floorPositionList)
            if(room._floorPositionList.Exists(s => (s.x == pos.x && s.y == pos.y) ||
            (s.x + offset == pos.x && s.y + offset == pos.y) ||
            (s.x - offset == pos.x && s.y - offset == pos.y)))
                return true;

        return false;
    }

    [System.Obsolete]
    public void SetTestPosition(Vector2Int position)
    {
        SpawnPosition = position;
    }
    [System.Obsolete]
    public bool RoomOverlape(DungeonRoom room)
    {
        if (PointOverlaped(room.SpawnPosition) ||
            PointOverlaped(room._tL) ||
            PointOverlaped(room._tR) ||
            PointOverlaped(room._bL) ||
            PointOverlaped(room._bR))
            return true;
        else
            return false;
    }
    [System.Obsolete]
    private bool PointOverlaped(Vector2Int point)
    {
        if ((point.x >= _tL.x && point.y <= _tL.y) ||
               (point.x <= _tR.x && point.y <= _tR.y) ||
               (point.x >= _bL.x && point.y >= _bL.y) ||
               (point.x <= _bR.x && point.y >= _bR.y))
            return true;
        else
            return false;
    }
}

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private DungeonTilemapVisualizer _visualizer;

    [SerializeField] private Vector2Int _dungeonSize;
    [SerializeField] [Min(1)] private int _roomsCount = 1;
    [SerializeField] [Range(1, 10)] private int _roomOffset = 1;

    [SerializeField] private List<DungeonRoomGeneration> _roomReferenceList;

    private List<DungeonRoom> _preparedRooms;

    private HashSet<Vector2Int> _floorPositions;
    private HashSet<Vector2Int> _wallPositions;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)_dungeonSize / 2, (Vector2)_dungeonSize);
        Gizmos.color = Color.green;
        if(_preparedRooms != null)
            foreach (var room in _preparedRooms)
                if(room.FloorPositions != null && room.FloorPositions.Count > 0)
                    Gizmos.DrawWireCube((Vector3Int)(room._min + room.Size / 2), (Vector3Int)room.Size);
    }

    private void Start()
    {
        if (_visualizer == null || _roomReferenceList == null || _roomReferenceList.Count == 0)
            return;

        GenerateDungeonFloor();
        GenerateDungeonWalls();

        InitializeDungeonSpawners();

        VisualizeDungeonTiles();

        _preparedRooms = null;
        _floorPositions = null;
        _wallPositions = null;
    }

    private void VisualizeDungeonTiles()
    {
        var pos = _preparedRooms.Find(s => s.RoomType == eDungeonRoomType.FinishRoom).SpawnPosition;
        var leaderPositions = new List<Vector2Int>() { pos + Vector2Int.left, pos + Vector2Int.right };

        _wallPositions.UnionWith(leaderPositions);
        leaderPositions.Add(pos);

        _visualizer.VisualizeFloorTiles(_floorPositions);
        _visualizer.VisualizeWallTiles(_wallPositions);
        _visualizer.VisualizeLeaderTiles(leaderPositions);
    }

    private void InitializeDungeonSpawners()
    {
        foreach (var room in _preparedRooms)
        {
            if (room.RoomType == eDungeonRoomType.DefaultRoom)
                room.Parameters.InitDungeonSpawners(room.FloorPositions);
            else if (room.RoomType == eDungeonRoomType.StartRoom)
                Player.Instance.transform.position = (Vector2)room.Parameters.GetRandomEmptyPosition(room.FloorPositions);
        }
    }

    private void GenerateDungeonWalls()
    {
        _wallPositions = new HashSet<Vector2Int>();

        foreach (var position in _floorPositions)
            foreach (var direction in Direction2D.FullDirectionsList)
            {
                var wallPos = Vector2Int.RoundToInt(position + direction);
                if (!_floorPositions.Contains(wallPos))
                    _wallPositions.Add(wallPos);
            }
    }

    private void GenerateDungeonFloor()
    {
        _floorPositions = new HashSet<Vector2Int>();
        _preparedRooms = new List<DungeonRoom>();

        _preparedRooms = BuildDungeonRooms();
        foreach (var room in _preparedRooms)
            _floorPositions.UnionWith(room.FloorPositions);

        var corridorList = GetCorridorsFloorPositions();
        _floorPositions.UnionWith(corridorList);
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currnetCenter, Vector2Int destinationCenter)
    {
        var corridorPositions = new HashSet<Vector2Int>();

        var position = currnetCenter;
        corridorPositions.Add(position);

        while (position.y != destinationCenter.y)
        {
            if (destinationCenter.y > position.y)
                position += Vector2Int.up;
            else if (destinationCenter.y < position.y)
                position += Vector2Int.down;

            corridorPositions.Add(position);
        }

        while (position.x != destinationCenter.x)
        {
            if (destinationCenter.x > position.x)
                position += Vector2Int.right;
            else if (destinationCenter.x < position.x)
                position += Vector2Int.left;

            corridorPositions.Add(position);
        }

        return corridorPositions;
    }

    private Vector2Int FindClosestCenter(Vector2Int currnetRoomCenter, List<Vector2Int> roomCenters)
    {
        var closestPoint = Vector2Int.zero;
        float currentDistance = int.MaxValue;

        foreach (var position in roomCenters)
        {
            var distance = Vector2Int.Distance(position, currnetRoomCenter);
            if (distance < currentDistance)
            {
                currentDistance = distance;
                closestPoint = position;
            }
        }

        return closestPoint;
    }

    private HashSet<Vector2Int> GetCorridorsFloorPositions()
    {
        var totalPositions = new HashSet<Vector2Int>();

        var roomCenters = new List<Vector2Int>();
        _preparedRooms.ForEach(s => roomCenters.Add(s.SpawnPosition));

        var currnetCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currnetCenter);

        while (roomCenters.Count > 0)
        {
            var closestCenter = FindClosestCenter(currnetCenter, roomCenters);
            roomCenters.Remove(closestCenter);

            var corridorPositions = CreateCorridor(currnetCenter, closestCenter);
            currnetCenter = closestCenter;

            totalPositions.UnionWith(corridorPositions);
        }

        return totalPositions;
    }

    private HashSet<Vector2Int> GetRoomsFloorPositions(DungeonRoom room, Vector2Int spawnPosition)
    {
        var roomFloorPositions = new HashSet<Vector2Int>();
        var currentPosition = spawnPosition;

        for (int i = 0; i < room.Parameters.iterations; i++)
        {
            var floor = new HashSet<Vector2Int>();
            floor.Add(currentPosition);

            var prevPosition = currentPosition;
            for (int j = 0; j < room.Parameters.floorPositionsCount; j++)
            {
                var randomPosition = prevPosition + Direction2D.GetRandomStandartDirection();
                prevPosition = Vector2Int.RoundToInt(randomPosition);
                floor.Add(prevPosition);
            }

            roomFloorPositions.UnionWith(floor);
            if (room.Parameters.isRandomEachIteration)
                currentPosition = roomFloorPositions.ElementAt(Random.Range(0, roomFloorPositions.Count));
        }

        return roomFloorPositions;
    }

    private DungeonRoom TrySpawnRoom(DungeonRoom room, int maxX, int maxY)
    {
        var x = Random.Range(0, maxX);
        var y = Random.Range(0, maxY);

        room.SetPosition(x, y);
        room.FloorPositions = GetRoomsFloorPositions(room, room.SpawnPosition);

        return room;
    }

    private List<DungeonRoom> BuildDungeonRooms()
    {
        var offset = new Vector2Int(_roomOffset, _roomOffset);

        var preparedRooms = new List<DungeonRoom>();
        for (int i = 0; i < _roomsCount; i++)
        {
            int rndIndex = Random.Range(0, _roomReferenceList.Count);
            var preparedRoom = new DungeonRoom(_roomReferenceList[rndIndex]);

            if (i == 0)
                preparedRoom.RoomType = eDungeonRoomType.StartRoom;
            else if (i == _roomsCount - 1)
                preparedRoom.RoomType = eDungeonRoomType.FinishRoom;

            preparedRooms.Add(preparedRoom);
        }

        var initedRooms = new List<DungeonRoom>();
        foreach (var prepRoom in preparedRooms)
        {
            int maxX = _dungeonSize.x - _roomOffset;
            int maxY = _dungeonSize.y - _roomOffset;

            var room = prepRoom;
            room = TrySpawnRoom(room, maxX, maxY);

            int debugIndex = 0;
            while (initedRooms.Exists(s => s.RoomOverlaped(room, _roomOffset)))
            {
                room = TrySpawnRoom(room, maxX, maxY);

                if (debugIndex++ == 100)
                    break;
            }

            initedRooms.Add(room);
        }

        return preparedRooms;
    }
}
