using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DungeonRoomGenerator
{
    public static HashSet<Vector2Int> CreateFloor(DungeonRoomParameters parameters, Vector2Int startPosition)
    {
        var floorPositions = new HashSet<Vector2Int>();
        var currentPosition = startPosition;

        for (int i = 0; i < parameters.iterations; i++)
        {
            var floor = DungeonGenerationAlgorithms.GetRandomFloorPositions(currentPosition, parameters.floorPositionsCount);
            floorPositions.UnionWith(floor);

            if (parameters.isRandomEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }

    public static void CreateWalls(HashSet<Vector2Int> floorPositions, DungeonTilemapVisualizer visualizer)
    {
        var wallsPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
            foreach (var direction in Direction2D.FullDirectionsList)
            {
                if (!floorPositions.Contains(position + direction))
                    wallsPositions.Add(position + direction);
            }

        visualizer.VisualizeWallTiles(wallsPositions);
    }
}