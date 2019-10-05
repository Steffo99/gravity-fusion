using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Merger : MonoBehaviour
{
    public Particle particle;
    protected List<Merger> mergeCandidates;

    protected Collider2D Collider {
        get {
            return particle.mergeCollider;
        }
    }

    protected int ParticlesToMerge {
        get {
            return particle.gameController.particlesToMerge;
        }
    }

    
    protected void Awake() {
        particle = GetComponentInParent<Particle>();
    }

    protected void Start() {
        mergeCandidates = new List<Merger>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Merger otherMerger = other.GetComponent<Merger>();
        if(otherMerger == null) return;
        Particle otherParticle = otherMerger.particle;
        if(this.particle.Tier != otherParticle.Tier) return;
        mergeCandidates.Add(otherMerger);
        if(CanMerge()) DoMerge();
    }

    private void OnTriggerExit2D(Collider2D other) {
        Merger otherMerger = other.GetComponent<Merger>();
        if(otherMerger == null) return;
        mergeCandidates.Remove(otherMerger);
    }

    protected bool CanMerge() {
        return mergeCandidates.Count >= ParticlesToMerge;
    }

    protected void DoMerge() {
        particle.Tier += 1;
        foreach(Merger merged in mergeCandidates.ToArray()) {
            Destroy(merged.particle.gameObject);
        }
    }
}
