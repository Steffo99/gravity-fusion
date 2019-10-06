using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOnMouseClick : MonoBehaviour
{
    [Header("Config")]
    public int mouseButton;
    public float pushForce;
    public float pushRadius;

    protected Vector3 GetWorldMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        if(Input.GetMouseButton(mouseButton)) {
            Vector3 mousePosition = GetWorldMousePosition();
            Collider2D[] affected = Physics2D.OverlapCircleAll(mousePosition, pushRadius);
            foreach(Collider2D collider in affected) {
                float distance = Vector3.Distance(mousePosition, collider.transform.position);
                Vector2 direction = (collider.transform.position - mousePosition).normalized;
                collider.attachedRigidbody?.AddForce(direction * pushForce / Mathf.Pow(distance, 2));
            }
        }
    }
}
