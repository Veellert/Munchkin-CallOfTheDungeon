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

    private void OnDestroy()
    {
        DestroySpawners(_spawners);
    }

    public static void InitSpawners(DungeonSpawnerParameters[] spawners)
    {
        foreach (var spawner in spawners)
        {
            spawner?.Spawn();
        }
    }

    public static void DestroySpawners(DungeonSpawnerParameters[] spawners)
    {
        foreach (var spawner in spawners)
        {
            spawner?.Destroy();
        }
    }
}
