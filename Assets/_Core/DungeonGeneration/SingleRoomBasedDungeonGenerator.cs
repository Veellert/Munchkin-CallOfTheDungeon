using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRoomBasedDungeonGenerator : DungeonGeneratorTemplate
{
    [SerializeField] private int _roomsCount = 1;
    [SerializeField] [Range(2, 5)] private int _roomOffset = 2;
    [Space]
    [SerializeField] [Range(1, byte.MaxValue)] private byte _roomsInRow = 4;

    protected override void StartGeneration()
    {
        var floorPositions = new HashSet<Vector2Int>();
        _currentVisualizePositions = floorPositions;

        var currentPosition = _startPosition;

        for (int i = 0; i < _roomsCount; i++)
        {
            floorPositions = GetParametersBasedPositions(_dungeonRoomParameters, currentPosition);

            int xMax = 0;
            int xMin = int.MaxValue;
            foreach (var position in floorPositions)
            {
                if (position.x > xMax)
                    xMax = position.x;
                if (position.x < xMin)
                    xMin = position.x;
            }

            currentPosition = new Vector2Int(xMax + ((xMax - xMin) * _roomOffset), currentPosition.y);

            if((i + 1) % _roomsInRow == 0)
            {
                currentPosition.x = _startPosition.x;
                currentPosition.y += ((xMax - xMin) * _roomOffset);
            }

            _currentVisualizePositions.UnionWith(floorPositions);
        }
        VisualizeDungeon();
    }
}
