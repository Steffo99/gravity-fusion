using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPan : MonoBehaviour
{
    public string axisName;

    protected Vector3? lastWorldPosition;

    protected GameController gameController;

    protected float panWasPressedFor;
    protected float? previousDistance;

    protected void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        lastWorldPosition = null;

        if (Application.platform == RuntimePlatform.Android)
        {
            Input.simulateMouseWithTouches = false;
        }

        panWasPressedFor = 0f;
    }

    private void Update()
    {
        bool panIsPressed = false;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                panIsPressed = Input.touchCount >= 2;
                break;
            default:
                panIsPressed = Input.GetAxisRaw(axisName) != 0f;
                break;
        }

        if(panIsPressed) {
            panWasPressedFor += Time.deltaTime;
        }
        else {
            panWasPressedFor = 0f;
        }

        if (Application.platform != RuntimePlatform.Android && panIsPressed || panWasPressedFor >= 0.2f)
        {
            float currentDistance;
            Vector2 currentScreenPosition;

            if (Application.platform == RuntimePlatform.Android)
            {
                Touch touchA = Input.GetTouch(0);
                Touch touchB = Input.GetTouch(1);
                Vector2 touchPositionTotal = touchA.position + touchB.position;
                currentScreenPosition = new Vector2(touchPositionTotal.x / Input.touchCount, touchPositionTotal.y / Input.touchCount);
            }
            else
            {
                currentScreenPosition = Input.mousePosition;
            }

            Vector3 currentWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenPosition);
            if (lastWorldPosition.HasValue)
            {
                Vector3 positionDelta = lastWorldPosition.Value - currentWorldPosition;
                Camera.main.transform.position += positionDelta;
                //Don't vibrate the camera!
                currentWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenPosition);
            }

            lastWorldPosition = currentWorldPosition;
        }
        else {
            lastWorldPosition = null;
        }
        
        if(Input.GetAxisRaw("ResetCamera") > 0) {
            if(gameController.blackHole != null) {
                Camera.main.transform.position = new Vector3(gameController.blackHole.transform.position.x,
                                                            gameController.blackHole.transform.position.y,
                                                            Camera.main.transform.position.z);
            } 
        }
    }
}
