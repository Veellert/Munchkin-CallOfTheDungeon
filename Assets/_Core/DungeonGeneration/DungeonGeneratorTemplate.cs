using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGeneratorTemplate : DungeonGenerator
{
    [SerializeField] protected DungeonRoomParameters _dungeonRoomParameters;
    [SerializeField] protected HashSet<Vector2Int> _currentVisualizePositions;

    protected override void StartGeneration()
    {
        
    }
    
    protected override void VisualizeDungeon()
    {
        if (_currentVisualizePositions == null)
            return;

        _visualizer.VisualizeFloorTiles(_currentVisualizePositions);
        WallGenerator.CreateWalls(_currentVisualizePositions, _visualizer);
    }
    
    protected HashSet<Vector2Int> GetParametersBasedPositions(DungeonRoomParameters parameters, Vector2Int position)
    {
        var floorPositions = new HashSet<Vector2Int>();
        var currentPosition = position;

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = DungeonGenerationAlgorithms.GetRandomFloorPositions(currentPosition, parameters.pathLength);
            floorPositions.UnionWith(path);

            if (parameters.isRandomEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }
}
