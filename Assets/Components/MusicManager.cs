using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public AudioSource baseLayer;
    public List<AudioSource> layers;

    protected bool neverStarted;

    protected void Start()
    {
        foreach(AudioSource audioSource in layers) {
            audioSource.volume = 0;
        }
        neverStarted = true;
    }

    public void UpdateLayers(int maxTier) {
        if(maxTier == -1) {
            baseLayer.volume = 1f;
        }
        else {
            baseLayer.volume = 0f;
        }

        if(neverStarted) {
            foreach(AudioSource layer in layers) {
                layer.Play();
            }
            neverStarted = false;
        }

        if(maxTier >= layers.Count) {
            foreach(AudioSource layer in layers) {
                layer.volume = 1f;
            }
        }
        else {
            for(int i = 0; i < layers.Count; i++) {
                if(maxTier >= i) {
                    layers[i].volume = 1f;
                }
                else {
                    layers[i].volume = 0f;
                }
            }
        }
    }
}
