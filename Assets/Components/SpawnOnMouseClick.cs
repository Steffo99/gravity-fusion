using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnMouseClick : MonoBehaviour
{
    [Header("Config")]
    public GameObject prefabToSpawn;
    public int mouseButton;

    protected Vector3 GetWorldMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        if(Input.GetMouseButton(mouseButton)) {
            Vector3 mousePosition = GetWorldMousePosition();
            GameObject instance = Instantiate(prefabToSpawn, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity);
        }
    }
}
