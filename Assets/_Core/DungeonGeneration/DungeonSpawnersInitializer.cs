using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawnersInitializer : MonoBehaviour
{
    [SerializeField] private DungeonSpawnerParameters[] _spawners;

    private void Start()
    {
        InitSpawners(_spawners);
    }

    public static void InitSpawners(DungeonSpawnerParameters[] spawners)
    {
        foreach (var spawner in spawners)
        {
            spawner?.Spawn();
        }
    }
}
