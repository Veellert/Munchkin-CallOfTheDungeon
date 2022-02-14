﻿/// <summary>
/// Класс "Половинка тайла"
/// </summary>
/// <remarks>
/// Используется для подсчета половинок тайлов
/// </remarks>
public class TileHalf
{
    /// <value>
    /// Размер половинки тайла => 0.5
    /// </value>
    public static float Size => 0.5f;

    /// <summary>
    /// Выдает размер ориентируясь на кол-во из параметра
    /// </summary>
    /// <param name="tileHalfCount">Количество половинок тайла</param>
    public static float GetTileHalf(float tileHalfCount) => Size * tileHalfCount;

    public float Value { get; private set; }

    public TileHalf()
    {
        Value = GetTileHalf(1);
    }

    public TileHalf(float tileHalfCount)
    {
        Value = GetTileHalf(tileHalfCount);
    }

    public static implicit operator float(TileHalf v) => v.Value;
}
