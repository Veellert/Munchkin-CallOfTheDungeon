using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Компонент отвечающий за визуализацию подземелья
    /// </summary>
    public class TilemapVisualizer : MonoBehaviour
    {
        [Header("Dungeon Floor")]
        [SerializeField] private Tilemap _floorTilemap;
        [SerializeField] private TileBase _floorTile;

        [Header("Dungeon Walls")]
        [SerializeField] private Tilemap _wallTilemap;
        [SerializeField] private TileBase _wallTile;

        [Header("Dungeon Finish")]
        [SerializeField] private Tilemap _finishTilemap;
        [SerializeField] private TileBase _finishTile;

        /// <summary>
        /// Визуализирует пол подземелья на определенных координатах
        /// </summary>
        /// <param name="positions">Координаты для визуализации</param>
        public void VisualizeFloorTiles(IEnumerable<Vector2Int> positions)
        {
            PaintTiles(positions, _floorTilemap, _floorTile);
        }

        /// <summary>
        /// Визуализирует стены подземелья на определенных координатах
        /// </summary>
        /// <param name="positions">Координаты для визуализации</param>
        public void VisualizeWallTiles(IEnumerable<Vector2Int> positions)
        {
            PaintTiles(positions, _wallTilemap, _wallTile);
        }

        /// <summary>
        /// Визуализирует переходы между локациями подземелья на определенных координатах
        /// </summary>
        /// <param name="positions">Координаты для визуализации</param>
        public void VisualizeFinishTiles(IEnumerable<Vector2Int> positions)
        {
            PaintTiles(positions, _finishTilemap, _finishTile);
        }

        /// <summary>
        /// Очищает все тайлмапы
        /// </summary>
        public void Clear()
        {
            _floorTilemap.ClearAllTiles();
            _wallTilemap.ClearAllTiles();
            _finishTilemap.ClearAllTiles();
        }

        /// <summary>
        /// Рисует тайлы на определенных координатах, тайлмапе и определенным тайлом
        /// </summary>
        /// <param name="positions">Координаты для визуализации</param>
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
        /// <param name="position">Координата для визуализации</param>
        /// <param name="tilemap">Тайлмап</param>
        /// <param name="tile">Тайл</param>
        private void PaintSingleTile(Vector2 position, Tilemap tilemap, TileBase tile)
        {
            tilemap.SetTile(tilemap.WorldToCell(position), tile);
        }
    }
}
