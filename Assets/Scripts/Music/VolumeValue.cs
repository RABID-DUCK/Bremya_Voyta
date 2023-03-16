using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource audioSrc;
    public float musicVolume = 1f;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        StaticVolume.musicVolume = vol;
        if (musicVolume == 0f)
        {
            audioSrc.Pause();
        }
        else
        {
            audioSrc.Play(); // в скобках был 0, убрал...
        }
    }
}
