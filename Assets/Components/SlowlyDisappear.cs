﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyDisappear : MonoBehaviour
{
    public float disappearAfter;
    public Gradient colorCurve;
    protected float timeLeft;
    protected SpriteRenderer sprite;

    protected float FractionLeft {
        get {
            return timeLeft / disappearAfter;
        }
    }

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        timeLeft = disappearAfter;
    }

    private void Update() {
        timeLeft -= Time.deltaTime;

        if(sprite != null) {
            sprite.color = colorCurve.Evaluate(FractionLeft);
        }

        if(timeLeft < 0) {
            Destroy(this.gameObject);
        }
    }
}