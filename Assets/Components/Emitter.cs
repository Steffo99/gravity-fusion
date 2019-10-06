using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle))]
public class Emitter : MonoBehaviour
{
    public float forceBase;
    public float forceVariance;
    protected int emittedParticles;

    public string particlePrefabName;
    protected GameObject particlePrefab;
    protected Particle particle;

    protected void Awake() {
        particle = GetComponent<Particle>();
        particlePrefab = (GameObject)Resources.Load(particlePrefabName);
    }

    protected void Start() {
        emittedParticles = 0;
        Invoke("Emit", 0.5f);
    }

    protected void Emit() {
        Invoke("Emit", 0.5f);
        if(particle.Tier < 1) return;
        GameObject newObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Particle newParticle = newObject.GetComponent<Particle>();
        newParticle.Tier = particle.Tier - 2;
        Vector3 direction = new Vector3(Mathf.Cos(Mathf.PI * emittedParticles / 3), Mathf.Sin(Mathf.PI * emittedParticles / 3), 0).normalized;
        float force = Mathf.Clamp(forceBase + ((Random.value - 0.5f) * forceVariance), 0f, float.PositiveInfinity);
        newParticle.rigidbody.AddForce(direction * force);
    }
}
