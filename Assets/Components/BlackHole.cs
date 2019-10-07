using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BlackHole : MonoBehaviour
{
    [BeforeStart]
    public float startingMass;
    [AfterStart]
    public float spentMass;

    public float Mass {
        get {
            return rigidbody.mass;
        }
        set {
            rigidbody.mass = value;
            Scale = Mathf.Sqrt(Mass / Mathf.PI);
        }
    }

    public float UnspentMass {
        get {
            return rigidbody.mass - spentMass;
        }
        set {
            spentMass = rigidbody.mass - value;
        }
    }

    public float Scale {
        get {
            return transform.localScale.x;
        }
        set {
            transform.localScale = new Vector3(value, value, 1);
        }
    }

    protected Rigidbody2D rigidbody;

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Start() {
        Mass = startingMass;
        spentMass = 0;
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        Particle otherParticle = other.GetComponent<Particle>();
        if(otherParticle != null) {
            Mass += otherParticle.Mass;
            Destroy(otherParticle.gameObject);
        }
    }
}
