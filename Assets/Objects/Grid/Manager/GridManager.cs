using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GridState
{
    Dev,
    Occupation,
    Temperature
}

public class GridManager : MonoBehaviour
{
    // Const & Static variables
    const float MAX_TEMP = 200;

    const string BACKGROUND_SORT_LAYER = "Background";
    const string OVERLAY_SORT_LAYER = "Overlay";

    const string GRID_MANAGER_TAG = "GridManager";

    public static GridManager Instance
    {
        private set;
        get;
    }

    /// Editor set variables

    public GameObject gridSquare;

    public Sprite devSprite;

    public Sprite occupationSprite;

    public int rowCount;
    public int colCount;

    public GridState selectedState;

    public float ambientTemperature;

    /// Script internal variables
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
        if (Input.GetButton("Overlay 1"))
        {
            ChangeOverlay((int)GridState.Dev);
        }
        else if (Input.GetButton("Overlay 2"))
        {
            ChangeOverlay((int)GridState.Occupation);
        }
        else if (Input.GetButton("Overlay 3"))
        {
            ChangeOverlay((int)GridState.Temperature);
        }
    }

    ///
    /// Publics
    ///
    public void SetOccupation(GridPoint point, GridSize size, Boolean occupied)
    {
        for(int x = point.x; x < point.x+size.x; x++)
        {
            for(int y = point.y; y < point.y+size.y; y++)
            {
                grid[x,y].GetComponent<GridSquareBehaviour>().occupied = occupied;
            }
        }
        foreach (SpriteRenderer gridSprite in GetComponentsInChildren<SpriteRenderer>())
        {
            gridSprite.sprite = occupationSprite;
            gridSprite.color =
                gridSprite.gameObject.GetComponent<GridSquareBehaviour>().occupied
                ? Color.red : Color.green;
        }
    }

    public bool GridIsFree(GridPoint point, GridSize size)
    {
        if (point.x < 0 || point.y < 0 ||
            point.x+size.x > rowCount ||
            point.y+size.y > colCount)
        {
            return false;
        }
        for(int x = point.x; x < point.x+size.x; x++)
        {
            for(int y = point.y; y < point.y+size.y; y++)
            {
                if (grid[x,y].GetComponent<GridSquareBehaviour>().occupied)
                {
                    return false;
                }
            }
        }
        return true;
    }

    ///
    /// Custom Callbacks
    ///

    public void ChangeOverlay(int selection)
    {
        if(this.enabled == false)
            throw new Exception("Callback called on inactive object. Is it linked to a prefab?");

        selectedState = (GridState)selection;
        switch (selectedState)
        {
            case GridState.Dev:
                // Grid squares are set as children when they are created (below)
                // so this gets the sprite renderers of all the grid squares
                foreach (SpriteRenderer gridSprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    gridSprite.sprite = devSprite;
                    gridSprite.color = Color.white;
                    gridSprite.sortingLayerName = BACKGROUND_SORT_LAYER;
                }
                break;
            case GridState.Occupation:
                // Grid squares are set as children when they are created (below)
                // so this gets the sprite renderers of all the grid squares
                foreach (SpriteRenderer gridSprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    gridSprite.sprite = occupationSprite;
                    gridSprite.color =
                        gridSprite.gameObject.GetComponent<GridSquareBehaviour>().occupied
                        ? Color.red : Color.green;
                    gridSprite.sortingLayerName = OVERLAY_SORT_LAYER;
                }
                break;
            case GridState.Temperature:
                // Grid squares are set as children when they are created (below)
                // so this gets the sprite renderers of all the grid squares
                foreach (SpriteRenderer gridSprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    gridSprite.sprite = occupationSprite;
                    var temp = gridSprite.gameObject.GetComponent<GridSquareBehaviour>().temperature;
                    var color = Color.white;
                    if (temp < 0)
                    {
                        color = new Color(0, 0, -1*temp/MAX_TEMP, 1);
                    } else if (temp > 0)
                    {
                        color = new Color(temp/MAX_TEMP, 0, 0, 1);
                    }
                    gridSprite.color = color;
                    gridSprite.sortingLayerName = OVERLAY_SORT_LAYER;
                }
                break;
            default:
                break;
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
                newObj.GetComponent<GridSquareBehaviour>().temperature = ambientTemperature;
                grid[c,r] = newObj;
            }
        }
    }
}
