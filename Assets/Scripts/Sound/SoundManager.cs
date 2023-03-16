using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
        }

        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("Не прокинут звук");
        }
    }

    public void PlaySound(List<AudioClip> clips)
    {
        foreach (AudioClip clip in clips)
        {
            PlaySound(clip);
        }
    }

    public void StopSound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
        }

        if (audioSource.clip != null)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.Log("Не прокинут звук");
        }
    }

    public void StopSound(List<AudioClip> clips)
    {
        foreach(AudioClip clip in clips)
        {
            StopSound(clip);
        }
    }
}
