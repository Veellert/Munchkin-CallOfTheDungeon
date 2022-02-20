using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Класс хранящий в себе информацию о монстрах в спавнере
    /// </summary>
    [System.Serializable]
    public class MonsterSpawnerEntity : SpawnerEntity
    {
        [Header("Monster")]
        [SerializeField] private BaseMonster _target;
        [Header("Count In Spawner")]
        [SerializeField] private int count = 1;

        public override void Spawn(Vector2Int spawnPosition)
        {
            if (_target)
                for (int i = 0; i < count; i++)
                    Object.Instantiate(_target, (Vector2)spawnPosition, Quaternion.identity);
        }
    }
}
