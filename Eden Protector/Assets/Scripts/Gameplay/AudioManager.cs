using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance => _instance;
    
    public AudioSource audioSource;

    private void Start()
    {
        if(_instance != null)
            Destroy(gameObject);
        _instance = this;
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
