using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GridState
{
    Dev,
    Occupation
}

public class GridManager : MonoBehaviour
{
    // Const & Static variables

    const string GRID_MANAGER_TAG = "GridManager";

    public static GridManager Instance
    {
        private set;
        get;
    }

    /// Editor set variables

    public GameObject gridSquare;

    public int rowCount;
    public int colCount;

    /// Script internal variables

    public GridState selectedState;

    private GameObject[,] grid;

    ///
    /// Event Handlers
    ///

    // Start is called before the first frame update
    void Awake()
    {
        Instance = GameObject.FindWithTag(GRID_MANAGER_TAG)
                            .GetComponent<GridManager>();

        CreateGameGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///
    /// Custom Callbacks
    ///

    public void ChangeOverlay(int selection)
    {
        if(this.enabled == false)
            throw new Exception("Callback called on inactive object. Is it linked to a prefab?");

        selectedState = (GridState)selection;

        // Grid squares are set as children when they are created (below)
        // so this gets the sprite renderers of all the grid squares
        foreach (GridSquareBehaviour gridSquare in GetComponentsInChildren<GridSquareBehaviour>())
        {
            gridSquare.State = selectedState;
        }
    }

    ///
    /// Publics
    ///
    
    public void SetGridOccupationStatus(Rect region, bool occupationStatus)
    {
        for (int x = (int)region.x; x < (int)region.xMax; x++)
        {
            for (int y = (int)region.y; y < (int)region.yMax; y++)
            {
                grid[x,y].GetComponent<GridSquareBehaviour>().Occupied = occupationStatus;
            }
        }
    }

    ///
    /// Privates
    ///

    private void CreateGameGrid()
    {
        grid = new GameObject[colCount, rowCount];

        for (int c = 0; c < colCount; c++)
        {
            for (int r = 0; r < rowCount; r++)
            {
                var newObj = Instantiate(gridSquare, new Vector3(c, r, 0), Quaternion.identity);
                newObj.transform.SetParent(this.transform);
                grid[c,r] = newObj;
            }
        }
    }
}
