using System;
using UnityEngine;

public struct GridPoint
{
    public readonly int x, y;

    public GridPoint(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static GridPoint FromVec(Vector3 v)
    {
        return new GridPoint((int)Math.Round(v.x), (int)Math.Round(v.y));
    }
}

public struct GridSize
{
    public readonly int x, y;

    public GridSize(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static GridSize FromVec(Vector3 v)
    {
        return new GridSize((int)Math.Round(v.x), (int)Math.Round(v.y));
    }
}
