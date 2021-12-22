using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHalf
{
    public static float Size => 0.5f;

    public static float GetTileHalf(float tileHalfCount) => Size * tileHalfCount;

    public float Value { get; private set; }

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
        Value = GetTileHalf(tileHalfCount);
    }

    public static implicit operator float(TileHalf v) => v.Value;
}
