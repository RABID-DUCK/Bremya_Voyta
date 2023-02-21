using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchVolume : MonoBehaviour
{
    private float musicVolume;
    private AudioSource audioSrc;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        musicVolume = StaticVolume.musicVolume;
        audioSrc.volume = musicVolume;
    }
}
