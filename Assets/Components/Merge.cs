using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Merge : MonoBehaviour
{
    [Header("Config")]
    public GameObject mergeIntoPrefab;

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
        mergeables.Add(otherMerge);

        if(CanMerge()) DoMerge();
    }

    private void OnTriggerExit2D(Collider2D other) {
        Merge otherMerge = other.GetComponent<Merge>();
        mergeables.Remove(otherMerge);
    }

    protected bool CanMerge() {
        return false;
    }

    protected void DoMerge() {
        GameObject mergeResult = Instantiate(mergeIntoPrefab, transform.position, Quaternion.identity);
        MergedInfo mergedInfo = mergeResult.AddComponent<MergedInfo>();
        mergedInfo.mergedCount = mergeables.Count;
        foreach(Merge merged in mergeables) {
            Rigidbody2D mergedBody = merged.GetComponentInParent<Rigidbody2D>();
            if(mergedBody != null) {
                mergedInfo.mergedMass += mergedBody.mass;
            }
            Destroy(merged.transform.parent);
        }
    }
}
