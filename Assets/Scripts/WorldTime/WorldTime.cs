using Photon.Pun;
using System;
using UnityEngine;

public class WorldTime : MonoBehaviourPunCallbacks
{
    [Space, Tooltip("Count of days elapsed")]
    public int countOfDaysElapsed; // Номер наступившего дня

    public float dayTimeInSeconds { get; } = 15f; // Количество секунд днем

    public float nightTimeInSeconds { get; } = 10f; // Количество секунд ночью

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

    public event Action OnStartTaxEvent = delegate { };
    public event Action OnStopTaxEvent = delegate { };

    public event Action OnStartCasinoEvent = delegate { };
    public event Action OnStopCasinoEvent = delegate { };

    public event Action OnStartEvent = delegate { };
    public event Action OnStopEvent = delegate { };

    public event Action OnEndGame = delegate { };

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
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    Hashtable ht = new Hashtable();
        //    ht.Add("StartTime", timeProgress);
        //    ht.Add("dayTime", dayTimeInSeconds);
        //    ht.Add("nightTime", nightTimeInSeconds);
        //    ht.Add("isCheckTimeOfDay", isCheckTimeOfDay);
        //    PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        //}
        //if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
        //{
        //    return;
        //}
        //else
        //{
        //    timeProgress = (float)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
        //}

        if (Application.isPlaying)
        {
            //if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("isCheckTimeOfDay"))
            //{
            //    if (isCheckTimeOfDay)
            //    {
            //        timeProgress += Time.fixedDeltaTime / dayTimeInSeconds;
            //    }
            //    else
            //    {
            //        timeProgress += Time.fixedDeltaTime / nightTimeInSeconds;
            //    }
            //}

            if (isStartTime)
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
                        countOfDaysElapsed++;
                    }
                }
            }

            EventSender(countOfDaysElapsed, timeProgress, isCheckTimeOfDay);
        }
        else
        {
            return;
        }
    }

    private void EventSender(int countOfDaysElapsed, float timeProgress, bool isCheckTimeOfDay)
    {
        //----------- Главные события ----------//
        OnGetTimeOfDay?.Invoke(isCheckTimeOfDay);

        OnGetNumberDay?.Invoke(countOfDaysElapsed);

        OnGetTimeProgress?.Invoke(timeProgress);
        //--------------------------------------//

        //--------------- Стандартные(Природные и т.п.) события ---------------//
        if (countOfDaysElapsed == 1 && timeProgress > 0.6f && isCheckTimeOfDay)
        {
            OnStartEvent?.Invoke();
        }

        if (countOfDaysElapsed == 2 && timeProgress > 1f && isCheckTimeOfDay)
        {
            OnStopEvent?.Invoke();
        }

        if (countOfDaysElapsed == 4 && timeProgress > 0.6f && isCheckTimeOfDay)
        {
            OnStartEvent?.Invoke();
        }

        if (countOfDaysElapsed == 5 && timeProgress > 1f && isCheckTimeOfDay)
        {
            OnStopEvent?.Invoke();
        }
        //----------------------------------------------------------------------//

        //---------------------------- Менялы -----------------------------//
        if (isCheckTimeOfDay && countOfDaysElapsed == 2 && timeProgress == 0f)
        {
            OnStartCasinoEvent?.Invoke();
        }

        if (isCheckTimeOfDay && countOfDaysElapsed == 2 && timeProgress == 0.8f)
        {
            OnStopCasinoEvent?.Invoke();
        }

        if (isCheckTimeOfDay && countOfDaysElapsed == 5 && timeProgress == 0f)
        {
            OnStartCasinoEvent?.Invoke();
        }

        if (isCheckTimeOfDay && countOfDaysElapsed == 5 && timeProgress == 0.5f)
        {
            OnStopCasinoEvent?.Invoke();
        }
        //------------------------------------------------------------------//

        //---------------------------------- Налоги -----------------------------------//
        if (countOfDaysElapsed == 5 && timeProgress == 0.5f && isCheckTimeOfDay == true)
        {
            OnStartTaxEvent?.Invoke();
        }
        else if (countOfDaysElapsed == 5 && timeProgress == 0.9f && isCheckTimeOfDay == true)
        {
            OnStopTaxEvent?.Invoke();
        }
        //-----------------------------------------------------------------------------//

        //---------------------- Конец игры ---------------------//
        if (countOfDaysElapsed == 5 && isCheckTimeOfDay == false)
        {
            OnEndGame?.Invoke();
        }
        //-------------------------------------------------------//
    }
}