using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// ��������� ���������� �� ������������ ����������
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
    /// ������������� ��� ���������� �� ������������ ������
    /// </summary>
    /// <param name="positions">����� ��� ������������</param>
    public void VisualizeFloorTiles(IEnumerable<Vector2Int> positions)
    {
        PaintTiles(positions, _floorTilemap, _floorTile);
    }

    /// <summary>
    /// ������������� ����� ���������� �� ������������ ������
    /// </summary>
    /// <param name="positions">����� ��� ������������</param>
    public void VisualizeWallTiles(IEnumerable<Vector2Int> positions)
    {
        PaintTiles(positions, _wallTilemap, _wallTile);
    }

    /// <summary>
    /// ������������� �������� ���������� �� ������������ ������
    /// </summary>
    /// <param name="positions">����� ��� ������������</param>
    public void VisualizeLeaderTiles(IEnumerable<Vector2Int> positions)
    {
        PaintTiles(positions, _leaderTilemap, _leaderTile);
    }

    /// <summary>
    /// ������� ��� ��������
    /// </summary>
    public void Clear()
    {
        _floorTilemap.ClearAllTiles();
        _wallTilemap.ClearAllTiles();
        _leaderTilemap.ClearAllTiles();
    }

    /// <summary>
    /// ������ ����� �� ������������ ������, �������� � ������������ ������
    /// </summary>
    /// <param name="positions">����� ��� ������������</param>
    /// <param name="tilemap">�������</param>
    /// <param name="tile">����</param>
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
            PaintSingleTile(position, tilemap, tile);
    }

    /// <summary>
    /// ������ ���� ���� �� ������������ �������� � ������������ ������
    /// </summary>
    /// <param name="position">����� ��� ������������</param>
    /// <param name="tilemap">�������</param>
    /// <param name="tile">����</param>
    private void PaintSingleTile(Vector2 position, Tilemap tilemap, TileBase tile)
    {
        tilemap.SetTile(tilemap.WorldToCell(position), tile);
    }
}