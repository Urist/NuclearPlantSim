using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    const string BACKGROUND_SORT_LAYER = "Background";
    const string OVERLAY_SORT_LAYER = "Overlay";

    public GameObject gridSquare;

    public Sprite devSprite;
    public Sprite occupationSprite;

    public int rowCount;
    public int colCount;

    public enum GridState
    {
        Dev,
        Occupation
    }

    ///
    /// Event Handlers
    ///

    // Start is called before the first frame update
    void Start()
    {
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

        GridState selectedState = (GridState)selection;

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
            default:
                break;
        }
    }

    ///
    /// Privates
    ///

    void CreateGameGrid()
    {
        for (int c = 0; c < colCount; c++)
        {
            for (int r = 0; r < rowCount; r++)
            {
                var newObj = Instantiate(gridSquare, new Vector3(r, c, 0), Quaternion.identity);
                newObj.transform.SetParent(this.transform);
            }
        }
    }
}
