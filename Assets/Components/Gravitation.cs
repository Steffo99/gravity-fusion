using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gravitation : MonoBehaviour
{    
    public bool isStatic;

    protected new Rigidbody2D rigidbody;
    protected GameController gameController;

    public List<Gravitation> SimulatedObjects {
        get {
            return gameController.simulatedObjects;
        }
    }

    public float Mass {
        get {
            return rigidbody.mass;
        }
    }

    public float GravitationConstant {
        get {
            return gameController.gravitationConstant;
        }
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnEnable() {
        SimulatedObjects.Add(this);
    }

    private void OnDisable() {
        SimulatedObjects.Remove(this);
    }

    // O(n²)
    private void FixedUpdate()
    {
        int positionInList = SimulatedObjects.IndexOf(this);
        foreach(Gravitation other in SimulatedObjects.Skip(positionInList)) {
            float distance = Vector3.Distance(this.transform.position, other.transform.position);
            float force = GravitationConstant * this.Mass * other.Mass / Mathf.Clamp(Mathf.Pow(distance, 2), 0.1f, float.PositiveInfinity);
            Vector3 direction = (other.transform.position - this.transform.position).normalized;
            if(!this.isStatic) rigidbody.AddForce(direction * force);
            if(!other.isStatic) other.rigidbody.AddForce(-direction * force);
        }
    }
}
