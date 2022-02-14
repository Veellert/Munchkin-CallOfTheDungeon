using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Класс отвечающий за поведение спавнера монстров
    /// </summary>
    [CreateAssetMenu(fileName = "NewMonsterSpawner", menuName = "Dungeon Generation/Room Spawners/Create Monster Spawner")]
    public class MonsterSpawner : Spawner
    {
        [Header("Monsters In Spawner")]
        [SerializeField] private List<MonsterSpawnerEntity> _monsterList = new List<MonsterSpawnerEntity>();

        public override void Spawn(Vector2Int spawnerPosition)
        {
            _monsterList.ForEach(monster => monster?.Spawn(spawnerPosition));
        }
    }
}