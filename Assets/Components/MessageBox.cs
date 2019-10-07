using System;
using UnityEngine;
using UnityEngine.UI;


public class MessageBox : MonoBehaviour {
    public float disappearanceSpeed = 0.22f;
    protected float opacity;

    protected Image image;
    protected Text text;
    protected bool nextTriggered;
    protected Action next;

    // Start is called before the first frame update
    protected void Awake()
    {
        text = GetComponent<Text>();
        image = transform.parent.GetComponentInChildren<Image>();
    }

    public void Write(string message, Action next) {
        text.text = message;
        this.next = next;
        nextTriggered = false;
        opacity = 1f;
    }

    protected void Update()
    {
        if(opacity > 0f) {
            opacity -= disappearanceSpeed * Time.deltaTime;
        }
        else {
            if(!nextTriggered)
            {
                nextTriggered = true;
                if(next != null) {
                    next();
                }
            }
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
        image.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }
}
