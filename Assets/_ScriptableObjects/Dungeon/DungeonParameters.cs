using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DungeonParameters : ScriptableObject
{
    [SerializeField] [HideInInspector] protected HashSet<Vector2Int> _preparedPositions;

    protected abstract void Generate(Vector2Int startPosition);
    protected abstract void Visualize();
    protected abstract void Clear();

    public void StartGeneration(Vector2Int currentPosition)
    {
        Generate(currentPosition); 
    }
    
    public void VisualizeDungeon(DungeonTilemapVisualizer visualizer)
    {
        if (_preparedPositions == null)
            return;

        ClearDungeon(visualizer);
        Visualize();

        visualizer.VisualizeFloorTiles(_preparedPositions);
        DungeonRoomGenerator.CreateWalls(_preparedPositions, visualizer);
    }

    public void ClearDungeon(DungeonTilemapVisualizer visualizer)
    {
        Clear();
        visualizer.Clear();
    }
}