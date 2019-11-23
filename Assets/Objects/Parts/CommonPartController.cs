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

    ///
    /// Event Handlers
    ///

    void Start()
    {
        SnapToGrid();
        UpdateOccupiedGridArea();
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
        renderer.sortingLayerName = DRAG_SORT_LAYER;
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
            if (keepPosition)
            {
                SnapToGrid();
                UpdateOccupiedGridArea();
            }
            else
            {
                // Restore to starting point
                transform.position = startPosition;
            }

            isDragging = false;

            // Restore it to the normal layer.
            renderer.sortingLayerName = DEFAULT_SORT_LAYER;
        }
    }

    private void SnapToGrid()
    {
        // Calulate grid offset
        float xsize = renderer.bounds.size.x;
        float ysize = renderer.bounds.size.y;

        // ASSUME: x and y sizes are integers.
        float xOffset = xsize % 2 == 0 ? GRID_SIZE * 0.5f : 0;
        float yOffset = ysize % 2 == 0 ? GRID_SIZE * 0.5f : 0;

        // Snap to grid
        transform.position = new Vector3(
                xOffset + GRID_SIZE * (float)Math.Round(transform.position.x / GRID_SIZE),
                yOffset + GRID_SIZE * (float)Math.Round(transform.position.y / GRID_SIZE),
                transform.position.z);

    }

    private void UpdateOccupiedGridArea()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (isDragging)
        {
            // Clear start area
            float xsize = spriteRenderer.bounds.size.x;
            float ysize = spriteRenderer.bounds.size.y;

            Rect area = new Rect(
                (float)Math.Ceiling(startPosition.x - xsize / 2.0f),
                (float)Math.Ceiling(startPosition.y - ysize / 2.0f),
                xsize,
                ysize);

            GridManager.Instance.SetGridOccupationStatus(area, false);

        }

        // Occupy current area
        float xsize = spriteRenderer.bounds.size.x;
        float ysize = spriteRenderer.bounds.size.y;

        Rect area = new Rect(
            (float)Math.Ceiling(transform.position.x - xsize / 2.0f),
            (float)Math.Ceiling(transform.position.y - ysize / 2.0f),
            xsize,
            ysize);

        GridManager.Instance.SetGridOccupationStatus(area, true);

    }

}
