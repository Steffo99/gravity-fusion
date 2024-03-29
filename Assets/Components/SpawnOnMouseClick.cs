﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnMouseClick : MonoBehaviour
{
    [Header("Config")]
    public int mouseButton = 0;
    public int spawnedTier = 0;
    public int spawnCount = 1;
    public float appliedForce = 0.1f;

    protected GameController gameController;

    protected void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    protected Vector3 GetWorldMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected void Update()
    {
        if(!gameController.upgradePanel.gameObject.activeSelf && Input.GetMouseButtonDown(mouseButton)) {
            Vector3 mousePosition = GetWorldMousePosition();
            if(gameController.blackHole == null) {
                gameController.blackHole = Instantiate(gameController.blackHolePrefab, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity).GetComponent<BlackHole>();
                gameController.tutorial.Step2();
                gameController.musicManager.UpdateLayers(-1);
            }
            else {
                for(int i = 0; i < spawnCount; i++) {
                    GameObject particleObject = Instantiate(gameController.particlePrefab, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity);
                    Particle particle = particleObject.GetComponent<Particle>();
                    particle.Tier = spawnedTier;
                    Vector2 direction = new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized;
                    particle.rigidbody.AddForce(direction * appliedForce);
                }
            }
        }
    }
}
