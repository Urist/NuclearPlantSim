using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPartController : MonoBehaviour
{
    const string DEFAULT_SORT_LAYER = "Default";
    const string DRAG_SORT_LAYER = "Drag";

    const float GRID_SIZE = 0.2f;

    private Vector3 offset;
    private Vector3 startPosition;
    private bool isDragging = false;

    ///
    /// Event Handlers
    ///

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
        Debug.Log($"Create '{this.name}'");
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
                // Check if offset is needed
                float xsize = GetComponent<SpriteRenderer>().bounds.size.x;
                // Snap to grid
                transform.position = new Vector3(
                        GRID_SIZE * (float)Math.Round(transform.position.x / GRID_SIZE),
                        GRID_SIZE * (float)Math.Round(transform.position.y / GRID_SIZE),
                        transform.position.z);
            }
            else
            {
                // Restore to starting point
                transform.position = startPosition;
            }

            isDragging = false;

            // Restore it to the normal layer.
            GetComponent<SpriteRenderer>().sortingLayerName = DEFAULT_SORT_LAYER;
        }
    }

}
