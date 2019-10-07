using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    protected float _health;    
    protected Particle particle;

    public float Health {
        get {
            return _health;
        }
    }

    protected void Awake() {
        particle = GetComponent<Particle>();
    }

    protected void Start() {
        ResetTimer();
    }

    public void ResetTimer() {
        _health = 1f;
    }

    protected void Update() {
        _health -= Mathf.Pow(particle.gameController.particleDurationConstant, particle.gameController.maxTierPresent - particle.Tier - 4) * Time.deltaTime;

        if(_health < 0) {
            Destroy(this.gameObject);
        }
    }
}
