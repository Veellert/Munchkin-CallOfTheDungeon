using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private DungeonTilemapVisualizer _visualizer;
    [SerializeField] private Vector2Int _startPosition;
    [SerializeField] private DungeonParameters _parameters;

    public void GenerateDungeon()
    {
        ClearDungeon();
        _parameters.StartGeneration(_startPosition);
        VisualizeDungeon();
    }
    
    public void ClearDungeon()
    {
        _parameters.ClearDungeon(_visualizer);
    }
    
    public void VisualizeDungeon()
    {
        _parameters.VisualizeDungeon(_visualizer);
    }
}
