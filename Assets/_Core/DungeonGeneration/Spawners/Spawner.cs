using System;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Объект родитель отвечающий за поведение спавнеров
    /// </summary>
    public abstract class Spawner : ScriptableObject
    {
        /// <summary>
        /// Создает спавнер на определенной точке
        /// </summary>
        /// <param name="spawnerPosition">Точка для спавна</param>
        public abstract void Spawn(Vector2Int spawnerPosition);
    }
}