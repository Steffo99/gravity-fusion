using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUpgradesToggler : MonoBehaviour
{
    protected GameController gameController;

    protected void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    protected void Update()
    {
        if(gameController.blackHole != null) {
            if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) {
                gameController.upgradePanel.gameObject.SetActive(!gameController.upgradePanel.gameObject.activeSelf);
            }
        }
    }
}
