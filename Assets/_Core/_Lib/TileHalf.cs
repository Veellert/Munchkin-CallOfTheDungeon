using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHalf
{
    public static float Size => 0.5f;

    public static float GetTileHalf(int tileHalfCount) => Size * tileHalfCount;

    public static float GetCells(float tileHalfCount) => Size * tileHalfCount;

    public float Value { get; set; }

    public TileHalf()
    {
        Value = GetTileHalf(1);
    }

    public TileHalf(int tileHalfCount)
    {
        Value = GetTileHalf(tileHalfCount);
    }

    public TileHalf(float tileHalfCount)
    {
        Value = GetCells(tileHalfCount);
    }

    public static implicit operator float(TileHalf v) => v.Value;
}
