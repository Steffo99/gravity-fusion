using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public float timeLeft;
    protected Particle particle;

    public float FractionLeft {
        get {
            return timeLeft / particle.Duration;
        }
    }

    protected void Awake() {
        particle = GetComponent<Particle>();
    }

    private void Start() {
        ResetTimer();
    }

    public void ResetTimer() {
        timeLeft = particle.Duration;
    }

    private void Update() {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 0) {
            Destroy(this.gameObject);
        }
    }
}
