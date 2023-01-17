using System;
using UnityEngine;
using Photon.Pun;

public class WorldTime : MonoBehaviour, IPunObservable
{
    [Space, Tooltip("Count of days elapsed")]
    public int countOfDaysElapsed; // Номер наступившего дня

    public float dayTimeInSeconds { get; set; }// Количество секунд днем

    public float nightTimeInSeconds { get; set; } // Количество секунд ночью

    public float timeProgress { get; set; } // Игровой прогресс

    /// <summary>
    /// Число дня, который наступил. Счет идет до 6, потом обнуляется!
    /// </summary>
    public event Action<int> GetNumberDay;

    /// <summary>
    /// Подвязываться к прогрессу веремени. Обнуляется, когда наступает день или ночь!
    /// </summary>
    public event Action<float> GetTimeProgress;

    /// <summary>
    /// Взять время суток!
    /// </summary>
    public event Action<bool> GetTimeOfDay;

    /// <summary>
    /// true - День, false - Ночь!
    /// </summary>
    public bool CheckTimeOfDay { get; set; }

    public bool IsStartTime { get; set; }// Отвечает за включение времени


    private void Awake()
    {
        CheckTimeOfDay = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        dayTimeInSeconds = 15f;
        nightTimeInSeconds = 60f;
        timeProgress = 0.2f;

        CheckTimeOfDay = true;

        GetNumberDay?.Invoke(countOfDaysElapsed);

        GetTimeProgress?.Invoke(timeProgress);

        GetTimeOfDay?.Invoke(CheckTimeOfDay);

        Coordinator.OnEndEducation += StartTime;
    }

    public void StartTime()
    {
        IsStartTime = true;
    }

    private void FixedUpdate()
    {
        if (IsStartTime)
        {
            ChengeOfTime();
        }
    }

    private void ChengeOfTime()
    {
        if (dayTimeInSeconds == 0)
        {
            print("There can't be 0 seconds in one day!!!");
        }

        if (Application.isPlaying)
        {
            if (CheckTimeOfDay)
            {
                timeProgress += Time.fixedDeltaTime / dayTimeInSeconds;
            }
            else
            {
                timeProgress += Time.fixedDeltaTime / nightTimeInSeconds;
            }
        }

        if (timeProgress > 1f)
        {
            timeProgress = 0f;

            CheckTimeOfDay = !CheckTimeOfDay;
            GetTimeOfDay?.Invoke(CheckTimeOfDay);

            if (CheckTimeOfDay)
            {
                countOfDaysElapsed++;

                if (countOfDaysElapsed > 6)
                {
                    countOfDaysElapsed = 0;
                }
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(timeProgress);
            Debug.Log(timeProgress);
        }
        else if (stream.IsReading)
        {
            timeProgress = (float)stream.ReceiveNext();
            Debug.Log("Reading: " + timeProgress);
        }
    }
}