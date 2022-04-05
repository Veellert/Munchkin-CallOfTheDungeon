using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// ����� ���������� �� ��������� �������� �������
    /// </summary>
    [CreateAssetMenu(fileName = "NewTraderSpawner", menuName = "Dungeon Generation/Room Spawners/Create Trader Spawner")]
    public class TraderSpawner : Spawner
    {
        [SerializeField] private BaseTrader _traderTarget;

        public override void Spawn(Vector2Int spawnerPosition)
        {
            Vector2 spawnPosition = new Vector2(spawnerPosition.x + 0.5f, spawnerPosition.y);
            Instantiate(_traderTarget, spawnPosition, Quaternion.identity);
        }
    }
}