using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnMouseClick : MonoBehaviour
{
    [Header("Config")]
    public GameController gameController;
    public int mouseButton;
    public int spawnedTier;
    public int spawnCount;
    public float appliedForce;

    protected void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    protected Vector3 GetWorldMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(mouseButton)) {
            Vector3 mousePosition = GetWorldMousePosition();
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
