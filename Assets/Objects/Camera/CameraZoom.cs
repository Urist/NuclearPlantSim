using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float ZOOM_SPEED;
    public float ZOOM_MAX;
    public float ZOOM_MIN;

    // Update is called once per frame
    void Update()
    {
        var camera = GetComponent<Camera>();
        
        float newSize = camera.orthographicSize + ZOOM_SPEED * Input.mouseScrollDelta.y;

        if (newSize > ZOOM_MAX)
            newSize = ZOOM_MAX;

        if (newSize < ZOOM_MIN)
            newSize = ZOOM_MIN;

        camera.orthographicSize = newSize;
    }
}
