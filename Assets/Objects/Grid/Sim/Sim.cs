using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sim : MonoBehaviour
{
    // Use FixedUpdate for simulation
    void FixedUpdate()
    {
        // Temperature
        for (int c = 1; c < GridManager.Instance.colCount-1; c++)
        {
            for (int r = 1; r < GridManager.Instance.rowCount-1; r++)
            {
                var tile = GridManager.Instance.grid[c,r].GetComponent<GridSquareBehaviour>();
                var surround = GetSurroundingTiles(c, r);
                var averageTemp = surround.Select(t => t.temperature).Sum() / surround.Count;
                tile.temperature = averageTemp;
            }
        }
    }

    List<GridSquareBehaviour> GetSurroundingTiles(int x, int y)
    {
        var tiles = new List<GridSquareBehaviour>(9);
        for(int dx = -1; dx <= 1; dx++)
        {
            for(int dy = -1; dy <= 1; dy++)
            {
                if (!(dx==0 && dy==0))
                {
                    tiles.Add(GridManager.Instance.grid[x+dx,y+dy].GetComponent<GridSquareBehaviour>());
                }
            }
        }
        return tiles;
    }
}