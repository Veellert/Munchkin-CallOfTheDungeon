using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, DungeonTilemapVisualizer visualizer)
    {
        visualizer.VisualizeWallTiles(FindWallsInDirections(floorPositions, Direction2D.FullDirectionsList));
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        var wallsPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
            foreach (var direction in directionsList)
            {
                if(!floorPositions.Contains(position + direction))
                    wallsPositions.Add(position + direction);
            }

        return wallsPositions;
    }
}
