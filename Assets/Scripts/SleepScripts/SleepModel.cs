using UnityEngine;
using UnityEngine.Video;

public class SleepModel : MonoBehaviour
{
    [SerializeField] protected WorldTime worldTime;

    public void GoSleep(VideoClip sleepVideo)
    {
        worldTime.isStartTime = false;

        UIController.ShowVideo(sleepVideo, AfterSleep);
    }

    public void AfterSleep()
    {
        worldTime.timeProgress = 0;
        worldTime.isCheckTimeOfDay = true;
        worldTime.countOfDaysElapsed++;
        worldTime.isStartTime = true;
    }
}