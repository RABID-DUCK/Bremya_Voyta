using Ekonomika.Dialog;
using System;
using UnityEngine;
using Photon.Pun;

public class WorldTime : MonoBehaviour, IPunObservable
{
    [Header("Time of day settings")]
    [Tooltip("Number of seconds during the day")]
    public static float dayTimeInSeconds; // Количество секунд днем
    [Tooltip("Number of seconds during the night")]
    public static float nightTimeInSeconds; // Количество секунд ночью
    [Tooltip("Time of day range")]
    public static float timeProgress; // Игровой прогресс

    [Space, Tooltip("Count of days elapsed")]
    public int countOfDaysElapsed; // Номер наступившего дня

    public bool IsStartTime; // Отвечает за включение времени

    /// <summary>
    /// Число дня, который наступил. Счет идет до 6, потом обнуляется!
    /// </summary>
    public static event Action<int> GetNumberDay;

    /// <summary>
    /// Подвязываться к прогрессу веремени. Обнуляется, когда наступает день или ночь!
    /// </summary>
    public static event Action<float> GetTimeProgress;

    /// <summary>
    /// Взять время суток!
    /// </summary>
    public static event Action<bool> GetTimeOfDay;

    /// <summary>
    /// true - День, false - Ночь!
    /// </summary>
    [HideInInspector] public static bool CheckTimeOfDay;

    private void Awake()
    {
        CheckTimeOfDay = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        dayTimeInSeconds = 180f;
        nightTimeInSeconds = 80f;
        timeProgress = 0f;
        CheckTimeOfDay = true;

        GetNumberDay?.Invoke(countOfDaysElapsed);

        GetTimeProgress?.Invoke(timeProgress);

        GetTimeOfDay?.Invoke(CheckTimeOfDay);

        DialogPresenter.OnDialogEnd += StartTime;
    }

    public void StartTime()
    {
        IsStartTime = true;
    }

    private void FixedUpdate()
    {
        //if (IsStartTime) // Раскомментировать условие, когда подключите диалоговую систему
        //{
        ChengeOfTime();
        //}
        /*        if (CheckTimeOfDay)
                {
                    Debug.Log("Day");
                }
                else
                {
                    Debug.Log("night");
                }*/
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
            Debug.Log("Reading: "+timeProgress);
        }
    }
}