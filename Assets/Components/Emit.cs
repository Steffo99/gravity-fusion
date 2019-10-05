using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle))]
public class Emitter : MonoBehaviour
{
    public GameObject particlePrefab;
    protected Particle particle;

    protected void Awake() {
        particle = GetComponent<Particle>();
    }

    protected void Start() {
        InvokeRepeating("Emit", 1f, 1f);
    }

    protected void Emit() {
        for(int i = 0; i < particle.Tier - 1; i++) {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
    }
}
