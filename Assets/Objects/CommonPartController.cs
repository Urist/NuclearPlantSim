using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPartController : MonoBehaviour
{
    const string DEFAULT_SORT_LAYER = "Default";
    const string DRAG_SORT_LAYER = "Drag";

    private Vector3 offset;

    void OnMouseDown()
    {
        // Save the offset of the mouse on the object (otherwise the object snaps to where the mouse is).
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Raise the dragged object above others, so you can see where you're dragging it.
        GetComponent<SpriteRenderer>().sortingLayerName = DRAG_SORT_LAYER;
    }
      
    void OnMouseDrag()
    {
        // Move the object, keeping it's z position unchanged.
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);;
    }

    void OnMouseUp()
    {
        // Drag ended. Restore it to the normal layer.
        GetComponent<SpriteRenderer>().sortingLayerName = DEFAULT_SORT_LAYER;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
        {
            // Losing focus also ends a drag. Restore it to the normal layer.
            GetComponent<SpriteRenderer>().sortingLayerName = DEFAULT_SORT_LAYER;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            // Pausing the app ends a drag too. Restore it to the normal layer.
            GetComponent<SpriteRenderer>().sortingLayerName = DEFAULT_SORT_LAYER;
        }
    }

}
