using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomWithScrollWheel : MonoBehaviour
{
    public float zoomMultiplier;
    public string zoomAxisName;

    protected Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        float mouseWheel = Input.GetAxisRaw(zoomAxisName);
        if(mouseWheel != 0) {
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - mouseWheel * zoomMultiplier, 0, float.PositiveInfinity);
        }
    }
}
