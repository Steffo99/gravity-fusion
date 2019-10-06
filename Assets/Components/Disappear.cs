using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public float health;
    protected Particle particle;

    protected void Awake() {
        particle = GetComponent<Particle>();
    }

    protected void Start() {
        ResetTimer();
    }

    public void ResetTimer() {
        health = 1f;
    }

    protected void Update() {
        health -= Mathf.Pow(particle.gameController.particleDurationConstant, particle.gameController.maxTierPresent - particle.Tier - 4) * Time.deltaTime;

        if(health < 0) {
            Destroy(this.gameObject);
        }
    }
}
