using Photon.Pun;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class WorldTime : MonoBehaviourPunCallbacks
{
    [Space, Tooltip("Count of days elapsed")]
    public int countOfDaysElapsed; // Номер наступившего дня

    public float dayTimeInSeconds { get; } = 360f; // Количество секунд днем

    public float nightTimeInSeconds { get; } = 60f; // Количество секунд ночью

    public float timeProgress { get; set; } // Игровой прогресс

    /// <summary>
    /// Число дня, который наступил. Счет идет до 6, потом обнуляется!
    /// </summary>
    public event Action<int> OnGetNumberDay;

    /// <summary>
    /// Подвязываться к прогрессу веремени. Обнуляется, когда наступает день или ночь!
    /// </summary>
    public event Action<float> OnGetTimeProgress;

    /// <summary>
    /// Взять время суток!
    /// </summary>
    public event Action<bool> OnGetTimeOfDay;

    /// <summary>
    /// true - День, false - Ночь!
    /// </summary>
    public bool isCheckTimeOfDay { get; set; }

    public bool isStartTime { get; set; } // Отвечает за включение времени

    public event Action OnEndGame;

    public event Action OnStartTaxEvent;

    public event Action OnStopTaxEvent;


    private void Awake()
    {
        isCheckTimeOfDay = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        timeProgress = 0f;

        isCheckTimeOfDay = true;

        OnGetNumberDay?.Invoke(countOfDaysElapsed);

        OnGetTimeProgress?.Invoke(timeProgress);

        OnGetTimeOfDay?.Invoke(isCheckTimeOfDay);

        Coordinator.OnEndEducation += StartTime;
    }

    public void StartTime()
    {
        isStartTime = true;
    }

    private void FixedUpdate()
    {
        if (isStartTime)
        {
            ChengeOfTime();
        }
    }

    public void ChengeOfTime()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartTime", timeProgress);
            ht.Add("dayTime", dayTimeInSeconds);
            ht.Add("nightTime", nightTimeInSeconds);
            ht.Add("isCheckTimeOfDay", isCheckTimeOfDay);
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        }
        if (PhotonNetwork.CurrentRoom.CustomProperties["StartTime"] == null)
        {
            return;
        }
        else
        {
            timeProgress = (float)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
        }

        if (dayTimeInSeconds == 0)
        {
            print("There can't be 0 seconds in one day!!!");
        }

        if (Application.isPlaying)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["isCheckTimeOfDay"] != null)
            {
                if (isCheckTimeOfDay)
                {
                    timeProgress += Time.fixedDeltaTime / dayTimeInSeconds;
                }
                else
                {
                    timeProgress += Time.fixedDeltaTime / nightTimeInSeconds;
                }
            }

            if (timeProgress == 0.5f && isCheckTimeOfDay == true)
            {
                OnStartTaxEvent?.Invoke();
            }
            else if (timeProgress == 0.8f && isCheckTimeOfDay == true)
            {
                OnStopTaxEvent?.Invoke();
            }

            if (timeProgress > 1f)
            {
                timeProgress = 0f;

                isCheckTimeOfDay = !isCheckTimeOfDay;
                OnGetTimeOfDay?.Invoke(isCheckTimeOfDay);

                if (isCheckTimeOfDay)
                {
                    countOfDaysElapsed++;

                    if (countOfDaysElapsed > 6)
                    {
                        OnEndGame?.Invoke();
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
}