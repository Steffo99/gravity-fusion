using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle))]
public class Emitter : MonoBehaviour
{
    public float forceBase;
    public float forceVariance;
    public float emissionPeriod;
    public int emissionQuantity;

    protected Particle particle;

    protected void Awake() {
        particle = GetComponent<Particle>();
    }

    protected void Start() {
        Invoke("Emit", emissionPeriod);
    }

    protected void Emit() {
        Invoke("Emit", emissionPeriod);
        if(particle.Tier < 2) return;
        for(int i = 0; i < emissionQuantity; i++) {
            GameObject newObject = Instantiate(particle.ParticlePrefab, transform.position, Quaternion.identity);
            Particle newParticle = newObject.GetComponent<Particle>();
            newParticle.Tier = particle.Tier - 2;
            Vector3 direction = new Vector3(Mathf.Cos(Mathf.PI * i * 2 / emissionQuantity), Mathf.Sin(Mathf.PI * i / emissionQuantity), 0).normalized;
            float force = Mathf.Clamp(forceBase + ((Random.value - 0.5f) * forceVariance), 0f, float.PositiveInfinity);
            newParticle.rigidbody.AddForce(direction * force);
        }
    }
}
