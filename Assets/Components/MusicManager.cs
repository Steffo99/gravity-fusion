using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public float changeSpeed = 1f;

    [BeforeStart]
    public AudioSource baseLayer;
    [BeforeStart]
    public AudioSource[] layers;

    protected bool neverStarted;

    protected float baseLayerTargetVolume;
    protected float[] targetVolume;

    protected void Start()
    {
        foreach(AudioSource audioSource in layers) {
            audioSource.volume = 0f;
        }
        targetVolume = new float[layers.Length];
        for(int i = 0; i < layers.Length; i++) {
            targetVolume[i] = 0f;
        }
        baseLayerTargetVolume = 1f;
        neverStarted = true;
    }

    protected GameController gameController;

    protected void Awake() {
        gameController = GetComponent<GameController>();
    }

    public void UpdateLayers(int maxTier) {
        if(gameController.blackHole == null) return;

        if(maxTier == -1) {
            baseLayerTargetVolume = 1f;
        }
        else {
            baseLayerTargetVolume = 0f;
        }

        if(neverStarted) {
            foreach(AudioSource layer in layers) {
                if(layer != null) {
                    layer.Play();
                }
            }
            neverStarted = false;
        }

        if(maxTier >= layers.Length) {
            for(int i = 0; i < layers.Length; i++) {
                targetVolume[i] = 1f;
            }
        }
        else {
            for(int i = 0; i < layers.Length; i++) {
                if(maxTier >= i) {
                    targetVolume[i] = 1f;
                }
                else {
                    targetVolume[i] = 0f;
                }
            }
        }
    }

    protected void Update() {
        if(baseLayer != null) {
            if(baseLayer.volume > baseLayerTargetVolume) {
                baseLayer.volume = Mathf.Clamp(baseLayer.volume - Time.deltaTime * changeSpeed, baseLayerTargetVolume, float.PositiveInfinity);
            }
            else if(baseLayer.volume < baseLayerTargetVolume) {
                baseLayer.volume = Mathf.Clamp(baseLayer.volume + Time.deltaTime * changeSpeed, 0f, baseLayerTargetVolume);
            }
        }

        for(int i = 0; i < layers.Length; i++) {
            if(layers[i] != null) {
                if(layers[i].volume > targetVolume[i]) {
                    layers[i].volume = Mathf.Clamp(layers[i].volume - Time.deltaTime * changeSpeed, targetVolume[i], float.PositiveInfinity);
                }
                else if(layers[i].volume < targetVolume[i]) {
                    layers[i].volume = Mathf.Clamp(layers[i].volume + Time.deltaTime * changeSpeed, 0f, targetVolume[i]);
                }   
            }
        }
    }

}
