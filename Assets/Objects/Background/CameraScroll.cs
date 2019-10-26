using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private GameObject mainCamera;

    private Vector3 offset;
    private Vector3 startPosition;

    private bool isDragging = false;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    void OnMouseDown()
    {
        // Save the starting position so it can be restored if necessary.
        // Vector3 is a struct which is a value type, so the '=' operation is a copy.
        startPosition = mainCamera.transform.position;
        isDragging = true;
        // Save the offset of the mouse on the object (otherwise the object snaps to where the mouse is).
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position;
        // Raise the dragged object above others, so you can see where you're dragging it.
    }
      
    void OnMouseDrag()
    {
        // Move the object, keeping it's z position unchanged.
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
        
        // TODO: Camera scrolling works, but is currently backwards. Fix that before enabling it.
        // mainCamera.transform.position = new Vector3(newPosition.x, newPosition.y, mainCamera.transform.position.z);

        // Save the offset of the mouse on the object (otherwise the object snaps to where the mouse is).
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position;
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

    private void Drop(bool keepPosition)
    {
        if (isDragging)
        {
            if (keepPosition == false)
            {
                // Restore to starting point
                mainCamera.transform.position = startPosition;
            }

            isDragging = false;
        }
    }
}
