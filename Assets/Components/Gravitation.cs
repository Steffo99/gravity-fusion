using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gravitation : MonoBehaviour
{    
    [Header("Config")]
    public bool isStatic;

    [Header("Forces")]
    protected Vector3 appliedForce;

    [Header("Internals")]
    public int positionInList;

    [Header("References")]
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
        positionInList = SimulatedObjects.Count;
        SimulatedObjects.Add(this);
    }

    private void OnDisable() {
        SimulatedObjects.Remove(this);
    }

    private void Start()
    {
        appliedForce = new Vector3(0f, 0f, 0f);
    }

    // O(n²)
    private void FixedUpdate()
    {
        foreach(Gravitation other in SimulatedObjects.Skip<Gravitation>(positionInList + 1)) {
            if(other.positionInList <= this.positionInList) continue;
            float distance = Vector3.Distance(this.transform.position, other.transform.position);
            float force = GravitationConstant * this.Mass * other.Mass / Mathf.Clamp(Mathf.Pow(distance, 2), 0.1f, float.PositiveInfinity);
            Vector3 direction = (other.transform.position - this.transform.position).normalized;
            if(!this.isStatic) rigidbody.AddForce(direction * force);
            if(!other.isStatic) other.rigidbody.AddForce(-direction * force);
        }
        appliedForce = new Vector3(0, 0, 0);
    }
}
