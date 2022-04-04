using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// ����� ���������� �� ��������� �������� �������
    /// </summary>
    [CreateAssetMenu(fileName = "NewChestSpawner", menuName = "Dungeon Generation/Room Spawners/Create Chest Spawner")]
    public class ChestSpawner : Spawner
    {
        [SerializeField] private BaseChest _chestTarget;

        public override void Spawn(Vector2Int spawnerPosition)
        {
            Vector2 spawnPosition = new Vector2(spawnerPosition.x + 0.5f, spawnerPosition.y + 0.5f);
            Instantiate(_chestTarget, spawnPosition, Quaternion.identity);
        }
    }
}