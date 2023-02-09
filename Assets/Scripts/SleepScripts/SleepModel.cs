using Ekonomika.Utils;
using UnityEngine;
using UnityEngine.Video;

public class SleepModel : MonoBehaviour
{
    public WorldTime worldTime;

    public void GoSleep(VideoClip sleepVideo)
    {
        UIController.ShowVideo(sleepVideo, AfterSleep);
    }

    public void AfterSleep()
    {
        worldTime.timeProgress = 0;
        worldTime.isCheckTimeOfDay = true;
        worldTime.countOfDaysElapsed++;

        CameraSwitch.SwichHouseCamera();
    }
}
