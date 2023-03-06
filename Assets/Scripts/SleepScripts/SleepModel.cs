using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Video;

public class SleepModel : MonoBehaviour
{
    [SerializeField] protected WorldTime worldTime;

    public void GoSleep(VideoClip sleepVideo)
    {
        worldTime.isStartTime = false;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable { { "isStartTime", 0 } });
        }

        UIController.ShowVideo(sleepVideo, AfterSleep);
    }

    public void AfterSleep()
    {
        worldTime.timeProgress = 0;
        worldTime.isCheckTimeOfDay = true;
        worldTime.countOfDaysElapsed++;
        worldTime.isStartTime = true;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable
            {
                {"StartTime", worldTime.timeProgress+0.002f},

                {"countOfDaysElapsed", worldTime.countOfDaysElapsed },

                {"isStartTime", worldTime.isStartTime ? 1 : 0 },

                {"isCheckTimeOfDay", worldTime.isCheckTimeOfDay ? 1 : 0 }
            });
        }

    }
}