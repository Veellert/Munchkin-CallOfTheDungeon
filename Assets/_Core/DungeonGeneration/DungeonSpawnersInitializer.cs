using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawnersInitializer : MonoBehaviour
{
    [SerializeField] private DungeonSpawnerParameters[] _spawners;

    private void Start()
    {
        foreach (var spawner in _spawners)
        {
            if (spawner.spawnerType == eDungeonSpawnerType.PlayerSpawner)
            {
                Instantiate(spawner.spawnTarget, (Vector3Int)spawner.spawnPosition, Quaternion.identity);
            }
        }
    }

}
