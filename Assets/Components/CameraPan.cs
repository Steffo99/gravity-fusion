using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPan : MonoBehaviour
{    
    public string axisName;

    protected new Camera camera;
    protected Vector3? lastMousePosition;

    private void Start() {
        lastMousePosition = null;
        camera = GetComponent<Camera>();
    }

    private void Update() {
        bool panIsPressed = Input.GetAxisRaw(axisName) != 0f;
        Vector3? currentMousePosition = null;
        if(panIsPressed) {
            currentMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            if(lastMousePosition.HasValue) {
                Vector3 positionDelta = lastMousePosition.Value - currentMousePosition.Value;
                camera.transform.position += positionDelta;
                currentMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        lastMousePosition = currentMousePosition;
    }
}
