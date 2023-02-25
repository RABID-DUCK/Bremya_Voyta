using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource audioSrc;
    public float musicVolume = 1f;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        StaticVolume.musicVolume = musicVolume;
        if (musicVolume == 0f)
        {
            audioSrc.Pause();
        }
        else
        {
            audioSrc.Play(0);
        }
    }
}