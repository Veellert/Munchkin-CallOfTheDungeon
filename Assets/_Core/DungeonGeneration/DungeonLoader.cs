using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLoader : MonoBehaviour
{
    [SerializeField] private DungeonGenerator _dungeonGenerator;

    private void Start()
    {
        _dungeonGenerator.ClearDungeon();
        _dungeonGenerator.GenerateDungeon();
    }
}
