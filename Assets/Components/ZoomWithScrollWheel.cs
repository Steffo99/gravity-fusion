using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomWithScrollWheel : MonoBehaviour
{
    [Header("Config")]
    public float zoomMultiplier;
    public string zoomAxisName;

    [Header("References")]
    protected new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        float mouseWheel = Input.GetAxis(zoomAxisName);
        if(mouseWheel != 0) {
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - mouseWheel * zoomMultiplier, 0, float.PositiveInfinity);
        }
    }
}
