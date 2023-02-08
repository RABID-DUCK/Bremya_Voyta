using System;
using UnityEngine;
using UnityEngine.Video;

public class SleepModel : MonoBehaviour
{
    public void GoSleep(VideoClip sleepVideo)
    {
        UIController.ShowVideo(sleepVideo, null);
    }

    public void AfterSleep(WorldTime worldTime, bool isICanSleep, bool isSleeping)
    {
        worldTime.timeProgress = 0;
        worldTime.isCheckTimeOfDay = true;

        isICanSleep = false;
        isSleeping = false;
    }
}
