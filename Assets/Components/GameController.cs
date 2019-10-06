using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameController : MonoBehaviour
{
    [Header("Constants")]
    public float gravitationConstant = 2;
    public int particlesToMerge = 5;
    public int scaleMultiplier = 3;
    public int particleDurationPerTier = 5;

    [Header("Big Bang")]
    public int bigBangParticles;
    public GameObject blackHolePrefab;

    [Header("Particles")]
    public GameObject particlePrefab;
    public Gradient[] tierGradients;
    public RuntimeAnimatorController[] tierAnimation;

    [Header("Upgrades")]
    public float[] upgradePushForce;
    public float[] upgradePushRadius;
    public float[] upgradeParticleCount;

    [Header("Bought Upgrades")]
    public int levelPush = 0;
    public int levelClick = 0;

    [Header("References")]
    public SpawnOnMouseClick spawner;
    public PushOnMouseClick pusher;
    public CameraPan panner;
    public MusicManager musicManager;

    protected void Awake() {
        spawner = Camera.main.GetComponent<SpawnOnMouseClick>();
        pusher = Camera.main.GetComponent<PushOnMouseClick>();
        panner = Camera.main.GetComponent<CameraPan>();
        musicManager = GetComponent<MusicManager>();
    }
}
