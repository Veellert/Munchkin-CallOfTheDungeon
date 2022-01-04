using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Компонент отвечающий за визуализацию подземелья
/// </summary>
public class DungeonTilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private TileBase _floorTile;

    [SerializeField] private Tilemap _wallTilemap;
    [SerializeField] private TileBase _wallTile;

    [SerializeField] private Tilemap _leaderTilemap;
    [SerializeField] private TileBase _leaderTile;

    /// <summary>
    /// Визуализирует пол подземелья на определенных точках
    /// </summary>
    /// <param name="positions">Точки для визуализации</param>
    public void VisualizeFloorTiles(IEnumerable<Vector2Int> positions)
    {
        PaintTiles(positions, _floorTilemap, _floorTile);
    }

    /// <summary>
    /// Визуализирует стены подземелья на определенных точках
    /// </summary>
    /// <param name="positions">Точки для визуализации</param>
    public void VisualizeWallTiles(IEnumerable<Vector2Int> positions)
    {
        PaintTiles(positions, _wallTilemap, _wallTile);
    }

    /// <summary>
    /// Визуализирует лестницы подземелья на определенных точках
    /// </summary>
    /// <param name="positions">Точки для визуализации</param>
    public void VisualizeLeaderTiles(IEnumerable<Vector2Int> positions)
    {
        PaintTiles(positions, _leaderTilemap, _leaderTile);
    }

    /// <summary>
    /// Очищает все тайлмапы
    /// </summary>
    public void Clear()
    {
        _floorTilemap.ClearAllTiles();
        _wallTilemap.ClearAllTiles();
        _leaderTilemap.ClearAllTiles();
    }

    /// <summary>
    /// Рисует тайлы на определенных точках, тайлмапе и определенным тайлом
    /// </summary>
    /// <param name="positions">Точки для визуализации</param>
    /// <param name="tilemap">Тайлмап</param>
    /// <param name="tile">Тайл</param>
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
            PaintSingleTile(position, tilemap, tile);
    }

    /// <summary>
    /// Рисует один тайл на определенном тайлмапе и определенным тайлом
    /// </summary>
    /// <param name="position">Точка для визуализации</param>
    /// <param name="tilemap">Тайлмап</param>
    /// <param name="tile">Тайл</param>
    private void PaintSingleTile(Vector2 position, Tilemap tilemap, TileBase tile)
    {
        tilemap.SetTile(tilemap.WorldToCell(position), tile);
    }
}