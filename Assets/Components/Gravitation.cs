using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gravitation : MonoBehaviour
{    
    [Header("Forces")]
    protected Vector3 appliedForce;

    [Header("Internals")]
    public int positionInList;
    public static List<Gravitation> simulatedObjects;

    [Header("References")]
    protected new Rigidbody2D rigidbody;
    protected GameController gameController;


    protected float mass {
        get {
            return rigidbody.mass;
        }

        set {
            rigidbody.mass = value;
        }
    }

    private void OnEnable() {
        if(simulatedObjects == null) {
            simulatedObjects = new List<Gravitation>();
        }
        positionInList = simulatedObjects.Count;
        simulatedObjects.Add(this);
    }

    private void OnDisable() {
        simulatedObjects.Remove(this);
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        appliedForce = new Vector3(0f, 0f, 0f);
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
            other.appliedForce -= direction * force;
        }
        rigidbody.AddForce(appliedForce);
        appliedForce = new Vector3(0, 0, 0);
    }
}
