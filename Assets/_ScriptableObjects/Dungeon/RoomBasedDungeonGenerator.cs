using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomDungeon", menuName = "Custom/DungeonGeneration/Dungeon/RoomBased")]
public class RoomBasedDungeonGenerator : DungeonParameters
{
    public int _roomsCount = 1;
    [Range(2, 5)] public int _roomOffset = 2;
    [Space]
    [Range(1, byte.MaxValue)] public byte _roomsInRow = 4;
    public DungeonRoomParameters _dungeonRoomParameters;
    
    private Vector2Int _currentPosition;

    protected override void Generate(Vector2Int startPosition)
    {
        var floorPositions = new HashSet<Vector2Int>();
        _preparedPositions = floorPositions;

        _currentPosition = startPosition;

        for (int i = 0; i < _roomsCount; i++)
        {
            _preparedPositions.UnionWith(CreateRoom(startPosition, i));
        }
    }

    protected override void Clear()
    {
        _dungeonRoomParameters.DestroyDungeonSpawners();
    }

    protected override void Visualize()
    {
        _dungeonRoomParameters.InitDungeonSpawners(_preparedPositions);
    }

    private HashSet<Vector2Int> CreateRoom(Vector2Int startPosition, int roomIndex)
    {
        var floorPositions = DungeonRoomGenerator.CreateFloor(_dungeonRoomParameters, _currentPosition);

        int xMax = 0;
        int xMin = int.MaxValue;
        foreach (var position in floorPositions)
        {
            if (position.x > xMax) xMax = position.x;
            if (position.x < xMin) xMin = position.x;
        }

        _currentPosition = new Vector2Int(xMax + ((xMax - xMin) * _roomOffset), _currentPosition.y);

        if ((roomIndex + 1) % _roomsInRow == 0)
        {
            _currentPosition.x = startPosition.x;
            _currentPosition.y += ((xMax - xMin) * _roomOffset);
        }

        return floorPositions;
    }
}
