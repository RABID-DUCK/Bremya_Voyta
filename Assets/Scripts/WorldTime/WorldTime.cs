using Photon.Pun;
using System;
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

    public event Action OnResetArrows = delegate { };

    public event Action IsSleepTime = delegate { };

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

        if (PhotonNetwork.IsMasterClient)
        {
            Coordinator.OnEndEducation += StartTime;
        }
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
                EventSender(countOfDaysElapsed, timeProgress, isCheckTimeOfDay);
            }
        }
    }

    private void EventSender(int countOfDaysElapsed, float timeProgress, bool isCheckTimeOfDay)
    {
        //----------- Главные события ----------//
        OnGetTimeOfDay?.Invoke(isCheckTimeOfDay);

        OnGetNumberDay?.Invoke(countOfDaysElapsed);

        OnGetTimeProgress?.Invoke(timeProgress);
        //--------------------------------------//

        //------- Сон -------//
        if (isCheckTimeOfDay == false && countOfDaysElapsed != 5)
        {
            IsSleepTime?.Invoke();
        }
        //-------------------//

        //--------------- Стандартные(Природные и т.п.) события ---------------//
        if (PhotonNetwork.IsMasterClient)
        {
            if (countOfDaysElapsed == 1 && timeProgress > 0.6f && isCheckTimeOfDay)
            {
                OnStartEvent?.Invoke();
            }

            if (countOfDaysElapsed == 4 && timeProgress > 0.6f && isCheckTimeOfDay)
            {
                OnStartEvent?.Invoke();
            }
        }

        if (countOfDaysElapsed == 2 && timeProgress > 0.95f && isCheckTimeOfDay)
        {
            OnStopEvent?.Invoke();
        }

        if (countOfDaysElapsed == 5 && timeProgress > 0.95f && isCheckTimeOfDay)
        {
            OnStopEvent?.Invoke();
        }
        //----------------------------------------------------------------------//

        //----------------------------- Менялы -----------------------------//
        if (isCheckTimeOfDay && countOfDaysElapsed > 2 && timeProgress == 0f)
        {
            OnStartCasinoEvent?.Invoke();
        }

        if (isCheckTimeOfDay && countOfDaysElapsed > 2 && timeProgress == 0.8f)
        {
            OnStopCasinoEvent?.Invoke();
        }

        if (isCheckTimeOfDay && countOfDaysElapsed > 5 && timeProgress == 0f)
        {
            OnStartCasinoEvent?.Invoke();
        }

        if (isCheckTimeOfDay && countOfDaysElapsed > 5 && timeProgress == 0.5f)
        {
            OnStopCasinoEvent?.Invoke();
        }
        //------------------------------------------------------------------//

        //---------------------------------- Налоги -----------------------------------//
        if (countOfDaysElapsed == 5 && timeProgress > 0.5f && isCheckTimeOfDay == true)
        {
            OnStartTaxEvent?.Invoke();
        }

        if (countOfDaysElapsed == 5 && timeProgress > 0.9f && isCheckTimeOfDay == true)
        {
            OnStopTaxEvent?.Invoke();
        }
        //-----------------------------------------------------------------------------// 

        //---------------------- Конец игры ---------------------//
        if (countOfDaysElapsed > 5 && isCheckTimeOfDay == false)
        {
            OnEndGame?.Invoke();
        }
        //-------------------------------------------------------//
    }
}