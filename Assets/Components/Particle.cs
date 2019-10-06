using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Gravitation))]
[RequireComponent(typeof(Emitter))]
public class Particle : MonoBehaviour {
    protected int _tier = 0;

    public new Rigidbody2D rigidbody;
    public Gravitation gravitation;
    public GameController gameController;
    public Merger merger;
    public Collider2D particleCollider;
    public Collider2D mergeCollider;
    public Emitter emitter;
    public Animator animator;
    public Hue hue;
    public SpriteRenderer mainRenderer;
    public SpriteRenderer auraRenderer;
    public SpriteRenderer detailsRenderer;

    public int Tier {
        get {
            return _tier;
        }
        set {
            _tier = value;
            Scale *= gameController.scaleMultiplier;
            animator.runtimeAnimatorController = gameController.tierAnimation[_tier];
            hue.PossibleColors = gameController.tierGradients[_tier];
        }
    }

    public float Scale {
        get {
            return transform.localScale.x;
        }
        set {
            transform.localScale = new Vector3(value, value, 1);
        }
    }

    public float Mass {
        get {
            return Mathf.Pow(gameController.particlesToMerge, Tier);
        }
    }

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody2D>(); 
        gravitation = GetComponent<Gravitation>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        merger = GetComponentInChildren<Merger>();
        particleCollider = GetComponent<Collider2D>();
        mergeCollider = merger.GetComponent<Collider2D>();
        emitter = GetComponent<Emitter>();
        mainRenderer = GetComponent<SpriteRenderer>();
        auraRenderer = transform.Find("Aura").GetComponent<SpriteRenderer>();
        detailsRenderer = transform.Find("Details").GetComponent<SpriteRenderer>();
        hue = GetComponent<Hue>();
        animator = GetComponent<Animator>();
    }

    protected void Start() {
        animator.runtimeAnimatorController = gameController.tierAnimation[_tier];
        hue.PossibleColors = gameController.tierGradients[_tier];
    }
}