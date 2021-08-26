using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonRoom", menuName = "Custom/DungeonGeneration/DungeonRoomParameters")]
public class DungeonRoomParameters : ScriptableObject
{
    public int iterations = 10;
    public int pathLength = 10;
    public bool isRandomEachIteration = true;
}
