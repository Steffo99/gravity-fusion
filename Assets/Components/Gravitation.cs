using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{

    public static List<Gravitation> simulatedObjects = null;
    
    protected new Rigidbody2D rigidbody;

    protected GameController gameController;

    public int positionInList;

    protected Vector3 appliedForce;

    protected float forcesIntensity;

    protected float mass {
        get {
            return rigidbody.mass;
        }

        set {
            rigidbody.mass = value;
        }
    }

    private void Awake() {
        if(simulatedObjects == null) {
            simulatedObjects = new List<Gravitation>();
        }
        positionInList = simulatedObjects.Count;
        simulatedObjects.Add(this);
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        appliedForce = new Vector3(0f, 0f, 0f);
        forcesIntensity = 0f;
    }

    // O(n²)
    private void FixedUpdate()
    {
        foreach(Gravitation other in simulatedObjects) {
            if(other.positionInList <= this.positionInList) continue;
            float distance = Vector3.Distance(this.transform.position, other.transform.position);
            float force = gameController.gravitationConstant * this.mass * other.mass / Mathf.Pow(distance, 2);
            Vector3 direction = (other.transform.position - this.transform.position).normalized;
            this.appliedForce += direction * force;
            this.forcesIntensity += force;
            other.appliedForce -= direction * force;
            other.forcesIntensity += force;
        }
        rigidbody.AddForce(appliedForce);
        if(forcesIntensity >= 5f) {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        appliedForce = new Vector3(0, 0, 0);
        forcesIntensity = 0f;
    }
}
