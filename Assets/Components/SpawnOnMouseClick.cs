using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnMouseClick : MonoBehaviour
{
    [Header("Config")]
    public int mouseButton = 0;
    public int spawnedTier = 0;
    public int spawnCount = 1;
    public float appliedForce = 0.1f;

    protected Camera camera;
    protected GameController gameController;

    protected void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        camera = GetComponent<Camera>();
    }

    protected void SpawnAtPosition(Vector3 position) {
        if(gameController.blackHole == null) {
                gameController.blackHole = Instantiate(gameController.blackHolePrefab, new Vector3(position.x, position.y, 0f), Quaternion.identity).GetComponent<BlackHole>();
                gameController.tutorial.Step2();
                gameController.musicManager.UpdateLayers(-1);
        }
        else {
            for(int i = 0; i < spawnCount; i++) {
                GameObject particleObject = Instantiate(gameController.particlePrefab, new Vector3(position.x, position.y, 0f), Quaternion.identity);
                Particle particle = particleObject.GetComponent<Particle>();
                particle.Tier = spawnedTier;
                Vector2 direction = new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized;
                particle.rigidbody.AddForce(direction * appliedForce);
            }
        }
    }

    protected void Update()
    {
        bool canSpawn = !gameController.upgradePanel.gameObject.activeSelf;
        if(Application.platform == RuntimePlatform.Android) {
            foreach(Touch touch in Input.touches) {
                if(touch.phase == TouchPhase.Began) {
                    SpawnAtPosition(camera.ScreenToWorldPoint(touch.position));
                }
            }
        }
        else {
            if(Input.GetMouseButtonDown(mouseButton)) {
                SpawnAtPosition(camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}
