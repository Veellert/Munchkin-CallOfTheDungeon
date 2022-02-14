using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Объект настроек комнаты
    /// </summary>
    [CreateAssetMenu(fileName = "NewRoomParameters", menuName = "Dungeon Generation/Room Parameters/Create New")]
    public class RoomGenerationParameters : ScriptableObject
    {
        [Header("Draw Path Iterations")]
        public int _pathIterations = 10;
        [Header("Path Length")]
        public int _pathLength = 10;
        [Header("Need Reassign Draw Point")]
        public bool _isRandomEachIteration = true;

        /// <returns>
        /// Координаты пола комнаты
        /// </returns>
        /// <param name="spawnPosition">Центр создания комнаты</param>
        public List<Vector2Int> InitFloor(Vector2Int spawnPosition)
        {
            var roomFloorPositions = new HashSet<Vector2Int>();
            var currentPosition = spawnPosition;

            for (int i = 0; i < _pathIterations; i++)
            {
                roomFloorPositions.UnionWith(DrawPathFrom(currentPosition));

                if (_isRandomEachIteration)
                    currentPosition = roomFloorPositions.ElementAt(Random.Range(0, roomFloorPositions.Count));
            }

            return roomFloorPositions.ToList();
        }

        /// <returns>
        /// Сгенерированный путь из координат
        /// </returns>
        /// <param name="startPosition">Начальная координата</param>
        private HashSet<Vector2Int> DrawPathFrom(Vector2Int startPosition)
        {
            var pathResult = new HashSet<Vector2Int> { startPosition };

            Vector2 prevPosition = startPosition;
            for (int i = 0; i < _pathLength; i++)
            {
                prevPosition = prevPosition + Direction2D.GetRandomStandartDirection();
                pathResult.Add(Vector2Int.RoundToInt(prevPosition));
            }

            return pathResult;
        }
    }
}