using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class MusicLayer {
    public AudioSource audioSource;

    public MusicLayer(AudioSource audioSource) {
        this.audioSource = audioSource;
    }

    public float Volume {
        get {
            return audioSource.volume;
        }
        set {
            audioSource.volume = value;
        }
    }
}


public class MusicManager : MonoBehaviour
{
    public List<MusicLayer> layers;

    void Start()
    {
        List<AudioSource> audioSources = new List<AudioSource>();
        GetComponentsInChildren<AudioSource>(true, audioSources);
        foreach(AudioSource audioSource in audioSources) {
            MusicLayer layer = new MusicLayer(audioSource);
            layer.Volume = 0;
        }
    }
}
