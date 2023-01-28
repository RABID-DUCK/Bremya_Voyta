using Photon.Pun;
using System;
using UnityEngine;

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

    public bool isStartTime { get; set; }// Отвечает за включение времени

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
        dayTimeInSeconds = 360f;
        nightTimeInSeconds = 60f;
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

    private void ChengeOfTime()
    {
        if (dayTimeInSeconds == 0)
        {
            print("There can't be 0 seconds in one day!!!");
        }

        if (Application.isPlaying)
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

        if(timeProgress == 0.5f && isCheckTimeOfDay == true)
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

            print(isCheckTimeOfDay);

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