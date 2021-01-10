using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPartController : MonoBehaviour
{
    const string DEFAULT_SORT_LAYER = "Default";
    const string DRAG_SORT_LAYER = "Drag";

    const float GRID_SIZE = 1.0f;

    private Vector3 offset;
    private Vector3 startPosition;
    private bool isDragging = false;

    private GridManager _gridManager = null;
    private GridManager gridManager {
        get {
            if (_gridManager == null) {
                _gridManager = GameObject.FindWithTag("GridManager").GetComponent<GridManager>();
            }
            return _gridManager;
        }
    }

    ///
    /// Event Handlers
    ///

    void Awake()
    {
        SnapToGrid();
    }

    void OnMouseDown()
    {
        // Save the starting position so it can be restored if necessary.
        // Vector3 is a struct which is a value type, so the '=' operation is a copy.
        startPosition = transform.position;
        isDragging = true;
        // Save the offset of the mouse on the object (otherwise the object snaps to where the mouse is).
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Raise the dragged object above others, so you can see where you're dragging it.
        GetComponent<SpriteRenderer>().sortingLayerName = DRAG_SORT_LAYER;
        // Set grid occupation status
        gridManager.SetOccupation(
            GetOrigin(),
            GetSize(),
            false
        );
    }
      
    void OnMouseDrag()
    {
        // Move the object, keeping it's z position unchanged.
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    void OnMouseUp()
    {
        // Drag ended. 
        Drop(true);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
        {
            // Losing focus also ends a drag.
            Drop(false);
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            // Pausing the app ends a drag too.
            Drop(false);
        }
    }

    ///
    /// Custom Callbacks
    ///

    public void Create()
    {
        Vector3 position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            0);

        Instantiate(this.gameObject, position, Quaternion.identity); 
    }

    ///
    /// Privates
    ///

    private void Drop(bool keepPosition)
    {
        if (isDragging)
        {
            if (keepPosition && gridManager.GridIsFree(GetOrigin(), GetSize()))
            {
                SnapToGrid();
            }
            else
            {
                // Restore to starting point
                transform.position = startPosition;
            }

            isDragging = false;

            // Restore it to the normal layer.
            GetComponent<SpriteRenderer>().sortingLayerName = DEFAULT_SORT_LAYER;

            // Set grid occupation status
            gridManager.SetOccupation(
                GetOrigin(),
                GetSize(),
                true
            );
        }
    }

    private void SnapToGrid()
    {
        // Calulate grid offset
        float xsize = GetComponent<SpriteRenderer>().bounds.size.x;
        float ysize = GetComponent<SpriteRenderer>().bounds.size.y;

        // ASSUME: x and y sizes are integers.
        float xOffset = xsize % 2 == 0 ? GRID_SIZE * 0.5f : 0;
        float yOffset = ysize % 2 == 0 ? GRID_SIZE * 0.5f : 0;

        // Snap to grid
        transform.position = new Vector3(
                xOffset + GRID_SIZE * (float)Math.Round(transform.position.x / GRID_SIZE),
                yOffset + GRID_SIZE * (float)Math.Round(transform.position.y / GRID_SIZE),
                transform.position.z);
    }

    private GridPoint GetOrigin()
    {
        float xsize = GetComponent<SpriteRenderer>().bounds.size.x;
        float ysize = GetComponent<SpriteRenderer>().bounds.size.y;

        return new GridPoint(
            (int)Math.Round(((transform.position.x-0.5) / GRID_SIZE) - (xsize/2))+1,
            (int)Math.Round(((transform.position.y-0.5) / GRID_SIZE) - (ysize/2))+1
        );
    }

    private GridSize GetSize()
    {
        return GridSize.FromVec(GetComponent<SpriteRenderer>().bounds.size);
    }

}
