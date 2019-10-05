using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PanWithMMB : MonoBehaviour
{    
    [Header("References")]
    protected new Camera camera;

    [Header("Internals")]
    protected Vector3? lastMousePosition;

    private void Start() {
        lastMousePosition = null;
        camera = GetComponent<Camera>();
    }

    private void Update() {
        bool mmbIsPressed = Input.GetMouseButton(2);
        Vector3? currentMousePosition = null;
        if(mmbIsPressed) {
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
