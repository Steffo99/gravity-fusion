using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPan : MonoBehaviour
{    
    public string axisName;

    protected Vector3? lastMousePosition;

    protected GameController gameController;

    protected void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start() {
        lastMousePosition = null;
    }

    private void Update() {
        bool panIsPressed = Input.GetAxisRaw(axisName) != 0f;
        Vector3? currentMousePosition = null;
        if(panIsPressed) {
            currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(lastMousePosition.HasValue) {
                Vector3 positionDelta = lastMousePosition.Value - currentMousePosition.Value;
                Camera.main.transform.position += positionDelta;
                currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        lastMousePosition = currentMousePosition;
        if(Input.GetAxisRaw("ResetCamera") > 0) {
            if(gameController.blackHole != null) {
                Camera.main.transform.position = new Vector3(gameController.blackHole.transform.position.x,
                                                             gameController.blackHole.transform.position.y,
                                                             Camera.main.transform.position.z);
            } 
        }
    }
}
