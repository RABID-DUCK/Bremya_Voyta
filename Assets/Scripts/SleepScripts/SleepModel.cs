using Ekonomika.Utils;
using UnityEngine;
using UnityEngine.Video;

public class SleepModel : MonoBehaviour
{
    [SerializeField] protected WorldTime worldTime;

    public void GoSleep(VideoClip sleepVideo, bool isSleep)
    {
        if (sleepVideo == true)
        {
            UIController.ShowVideo(sleepVideo, AfterSleep);
        }
        else
        {
            UIController.ShowVideo(sleepVideo, AfterEmergencySleep);
        }
    }

    public void AfterSleep()
    {
        worldTime.timeProgress = 0;
        worldTime.isCheckTimeOfDay = true;
        worldTime.countOfDaysElapsed++;

        CameraSwitch.SwichHouseCamera();
    }

    public void AfterEmergencySleep()
    {
        worldTime.timeProgress = 0;
        worldTime.isCheckTimeOfDay = true;
        worldTime.countOfDaysElapsed++;
    }
}
