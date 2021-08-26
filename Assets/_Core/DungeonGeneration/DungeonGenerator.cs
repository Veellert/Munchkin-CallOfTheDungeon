using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonGenerator : MonoBehaviour
{
    [SerializeField] protected DungeonTilemapVisualizer _visualizer = null;
    [SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        ClearDungeon();
        StartGeneration();
    }
    
    public void ClearDungeon()
    {
        _visualizer.Clear();
    }
    
    public void ReVisualizeDungeon()
    {
        VisualizeDungeon();
    }

    protected abstract void StartGeneration();
    protected abstract void VisualizeDungeon();
}
