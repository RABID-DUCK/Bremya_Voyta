using System;
using UnityEngine;
using UnityEngine.Video;

public class SleepModel : MonoBehaviour
{
    [SerializeField] private VideoClip sleepVideo;

    public event Action OnEndSleep;

    public void GoSleep()
    {
        UIController.ShowVideo(sleepVideo, SendEndSleep);
    }

    public void AfterSleep(WorldTime worldTime)
    {
        worldTime.timeProgress = 0;
        worldTime.CheckTimeOfDay = true;
    }

    private void SendEndSleep()
    {
        OnEndSleep?.Invoke();
    }

    public void ResetIsSleeping(bool isSleep)
    {
        isSleep = false;
    }
}
