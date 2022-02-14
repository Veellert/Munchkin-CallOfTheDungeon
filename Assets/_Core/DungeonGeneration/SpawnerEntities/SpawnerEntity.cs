using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Класс родитель хранящий в себе информацию о сущности в спавнере
    /// </summary>
    public abstract class SpawnerEntity
    {
        /// <summary>
        /// Спавнит сущность на определенной точке
        /// </summary>
        /// <param name="spawnPosition">Точка для спавна</param>
        public abstract void Spawn(Vector2Int spawnPosition);
    }
}
