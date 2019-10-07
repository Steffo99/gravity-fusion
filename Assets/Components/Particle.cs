using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Particle : MonoBehaviour {
    protected int _tier = 0;

    [HideInInspector]
    public new Rigidbody2D rigidbody;
    [HideInInspector]
    public Gravitation gravitation;
    [HideInInspector]
    public GameController gameController;
    [HideInInspector]
    public Merger merger;
    [HideInInspector]
    public Collider2D particleCollider;
    [HideInInspector]
    public Collider2D mergeCollider;
    [HideInInspector]
    public Emitter emitter;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Hue hue;
    [HideInInspector]
    public SpriteRenderer mainRenderer;
    [HideInInspector]
    public SpriteRenderer auraRenderer;
    [HideInInspector]
    public SpriteRenderer detailsRenderer;
    [HideInInspector]
    public Disappear disappear;

    public int Tier {
        get {
            return _tier;
        }
        set {
            _tier = value;
            Scale = Mathf.Pow(gameController.scaleMultiplier, _tier);
            animator.runtimeAnimatorController = gameController.tierAnimation[_tier % gameController.tierAnimation.Length];
            hue.PossibleColors = gameController.tierGradients[_tier % gameController.tierGradients.Length];
            gameController.CheckNewMaxTier(_tier);
            disappear.ResetTimer();
        }
    }

    public float Scale {
        get {
            return transform.localScale.x;
        }
        protected set {
            transform.localScale = new Vector3(value, value, 1);
        }
    }

    public float Mass {
        get {
            return Mathf.Pow(gameController.particlesToMerge + 1, Tier);
        }
    }

    public GameObject ParticlePrefab {
        get {
            return gameController.particlePrefab;
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
        disappear = GetComponent<Disappear>();
    }

    protected void Start() {
        animator.runtimeAnimatorController = gameController.tierAnimation[_tier % gameController.tierAnimation.Length];
        hue.PossibleColors = gameController.tierGradients[_tier % gameController.tierGradients.Length];
    }
    
    protected void OnDestroy() {
        if(Tier >= gameController.maxTierPresent) {
            gameController.RecalculateMaxTier();
        }
    }
}