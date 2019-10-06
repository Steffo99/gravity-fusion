using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Merger : MonoBehaviour
{
    [HideInInspector]
    public Particle particle;
    public float mergeAfterSeconds;
    
    protected bool mergeEnabled;

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
        mergeCandidates = new List<Merger>();
    }

    protected void Start() {
        mergeEnabled = false;
        Invoke("EnableMerge", mergeAfterSeconds);
    }

    protected void EnableMerge() {
        mergeEnabled = true;
        if(CanMerge()) DoMerge();
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        Merger otherMerger = other.GetComponent<Merger>();
        if(otherMerger == null) return;
        Particle otherParticle = otherMerger.particle;
        if(this.particle.Tier != otherParticle.Tier) return;
        mergeCandidates.Add(otherMerger);
        if(CanMerge()) DoMerge();
    }

    protected void OnTriggerExit2D(Collider2D other) {
        Merger otherMerger = other.GetComponent<Merger>();
        if(otherMerger == null) return;
        mergeCandidates.Remove(otherMerger);
    }

    protected bool CanMerge() {
        int mergeableCount = 0;
        foreach(Merger mergeCandidate in mergeCandidates) {
            if(mergeCandidate.mergeEnabled) mergeableCount += 1;
        }
        return mergeEnabled && mergeableCount >= ParticlesToMerge;
    }

    protected void DoMerge() {
        particle.Tier += 1;
        foreach(Merger merged in mergeCandidates.ToArray()) {
            if(merged == null) continue;
            Destroy(merged.particle.gameObject);
        }
        mergeCandidates.Clear();
        particle.mergeCollider.enabled = false;
        particle.mergeCollider.enabled = true;
    }
}
