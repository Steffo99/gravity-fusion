using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Merge : MonoBehaviour
{
    [Header("Config")]
    public GameObject mergeIntoPrefab;
    public bool setMergedMassFromTotal;

    [Header("References")]
    protected new CircleCollider2D collider;

    [Header("Internals")]
    protected List<Merge> mergeables;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        mergeables = new List<Merge>();
        mergeables.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Merge otherMerge = other.GetComponent<Merge>();
        if(otherMerge != null) {
            mergeables.Add(otherMerge);
            if(CanMerge()) DoMerge();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Merge otherMerge = other.GetComponent<Merge>();
        if(otherMerge != null) {
            mergeables.Remove(otherMerge);
        }
    }

    protected bool CanMerge() {
        return mergeables.Count >= 5;
    }

    protected void DoMerge() {
        GameObject mergeResult = Instantiate(mergeIntoPrefab, transform.position, Quaternion.identity);
        MergedInfo mergedInfo = mergeResult.AddComponent<MergedInfo>();
        mergedInfo.mergedCount = mergeables.Count;

        foreach(Merge merged in mergeables.ToArray()) {
            Rigidbody2D mergedBody = merged.GetComponentInParent<Rigidbody2D>();
            if(mergedBody != null) {
                mergedInfo.mergedMass += mergedBody.mass;
            }
            Destroy(merged.transform.parent.gameObject);
        }
        
        if(setMergedMassFromTotal) {
            Rigidbody2D mergeRigidbody = mergeResult.GetComponent<Rigidbody2D>();
            if(mergeRigidbody != null) {
                mergeRigidbody.useAutoMass = false;
                mergeRigidbody.mass = mergedInfo.mergedMass;
            }
        }
    }
}
