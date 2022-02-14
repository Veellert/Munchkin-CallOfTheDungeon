using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Класс хранящий в себе настройки генерации комнаты подземелья
    /// </summary>
    [System.Serializable]
    public class RoomPreferences
    {
        [Header("Room Build Parameters")]
        [SerializeField] private RoomGenerationParameters _roomParameters;
        [Header("Spawners In Room")]
        [SerializeField] private List<Spawner> _spawnerList = new List<Spawner>();

        /// <returns>
        /// Координаты пола комнаты
        /// </returns>
        /// <param name="spawnPosition">Центр создания комнаты</param>
        public List<Vector2Int> GetRoomFloor(Vector2Int spawnPosition)
        {
            return _roomParameters.InitFloor(spawnPosition);
        }

        /// <summary>
        /// Расставляет спавнеры на позиции и спавнит сущности
        /// </summary>
        /// <returns>
        /// Оставшиеся свободные координаты
        /// </returns>
        /// <param name="spawnPositions">Список точек для спавна</param>
        public List<Vector2Int> InitSpawners(List<Vector2Int> spawnPositions)
        {
            foreach (var spawner in _spawnerList)
            {
                var position = GetRandomEmptyPosition(spawnPositions);
                spawner.Spawn(position);
                spawnPositions.RemoveAll(s => s == position);
            }

            return spawnPositions;
        }

        /// <returns>
        /// Рандомная свободная точка из списка
        /// </returns>
        /// <param name="spawnPositions">Список точек для спавна</param>
        public Vector2Int GetRandomEmptyPosition(IEnumerable<Vector2Int> spawnPositions)
        {
            var randIndex = Random.Range(0, spawnPositions.Count());
            var position = spawnPositions.ElementAt(randIndex);

            while (!Direction2D.ExistEmptySpace(spawnPositions, position, true))
            {
                randIndex = Random.Range(0, spawnPositions.Count());
                position = spawnPositions.ElementAt(randIndex);
            }

            return position;
        }
    }
}
