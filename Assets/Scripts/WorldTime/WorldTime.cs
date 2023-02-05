using Photon.Pun;
using System;
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
    // держи блять. Один хуй он выоплняет код на 91 строке при ЛЮБЫХ СУКА УСЛОВИЯХ. Всё работает в игре, то что выводит эту ошибку это нормально
    // посмотри любой сайт ААА уровня и там ошибки спамит постоянно пока что-то не произойдёт чтобы они исчезли(а именно клик куда-то).
    // Специально для тебя поставил защиту так сказать "на всякий пожарный" и меня не ебёт что там спамит, это нормально, всё работает? Работает! Иди нахуй!
        if (PhotonNetwork.IsMasterClient)
        {
            Hashtable ht = new Hashtable { { "StartTime", timeProgress } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        }
        else if(!PhotonNetwork.IsMasterClient && isStartTime)
        {
            timeProgress = (float)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
        }

        if (!PhotonNetwork.IsMasterClient && !isStartTime) timeProgress = 0f;

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
}