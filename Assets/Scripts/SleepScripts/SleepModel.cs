using System;
using UnityEngine;
using UnityEngine.Video;

public class SleepModel : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GoSleep(VideoClip sleepVideo)
    {
        UIController.ShowVideo(sleepVideo, null);
    }

    public void AfterSleep(WorldTime worldTime)
    {
        worldTime.timeProgress = 0;
        worldTime.CheckTimeOfDay = true;
    }

    public void ResetIsSleeping(bool isSleep)
    {
        isSleep = false;
    }
}
