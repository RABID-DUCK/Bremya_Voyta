using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class WorldTime : MonoBehaviour
{
    [Space, Tooltip("Count of days elapsed")]
    public int countOfDaysElapsed; // Номер наступившего дня

    public float dayTimeInSeconds = 360f; // Количество секунд днем

    public float nightTimeInSeconds = 60f; // Количество секунд ночью

    public float timeProgress { get; set; } // Игровой прогресс

    /// <summary>
    /// true - День, false - Ночь!
    /// </summary>
    public bool isCheckTimeOfDay { get; set; }

    public bool isStartTime { get; set; } // Отвечает за включение времени

    [SerializeField] private WorldTimeEventSender eventSender;

    private void Start()
    {
        timeProgress = 0f;

        isCheckTimeOfDay = true;

        if (PhotonNetwork.IsMasterClient)
        {
            Coordinator.OnEndEducation += StartTime;
        }

        eventSender.EventSender(countOfDaysElapsed, timeProgress, isCheckTimeOfDay);
    }

    public void StartTime()
    {
        isStartTime = true;
    }

    private void FixedUpdate()
    {
        if (isStartTime)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                ChengeOfTime();
                Hashtable ht = new Hashtable {
                {"StartTime", timeProgress+0.002f},
                {"countOfDaysElapsed", countOfDaysElapsed },
                {"isStartTime", isStartTime ? 1 : 0 },
                {"isCheckTimeOfDay", isCheckTimeOfDay ? 1 : 0 }};
                PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
                return;
            }
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
            {
                timeProgress = (float)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
                isStartTime = (int)PhotonNetwork.CurrentRoom.CustomProperties["isStartTime"] != 0 ? true : false;
                isCheckTimeOfDay = (int)PhotonNetwork.CurrentRoom.CustomProperties["isCheckTimeOfDay"] != 0 ? true : false;
                countOfDaysElapsed = (int)PhotonNetwork.CurrentRoom.CustomProperties["countOfDaysElapsed"];
            }
            else
            {
                return;
            }

            ChengeOfTime();
        }
    }

    public void ChengeOfTime()
    {
        if (Application.isPlaying)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (isCheckTimeOfDay)
                {
                    timeProgress += Time.fixedDeltaTime / dayTimeInSeconds;
                }
                else
                {
                    timeProgress += Time.fixedDeltaTime / nightTimeInSeconds;
                }

                if (timeProgress > 1f)
                {
                    timeProgress = 0f;

                    isCheckTimeOfDay = !isCheckTimeOfDay;

                    if (isCheckTimeOfDay)
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            countOfDaysElapsed++;

                            //if (countOfDaysElapsed == 5 && isCheckTimeOfDay == false)
                            //{
                            //    isStartTime = false;
                            //}
                        }
                    }
                }
                eventSender.EventSender(countOfDaysElapsed, timeProgress, isCheckTimeOfDay);
            }
        }
    }
}