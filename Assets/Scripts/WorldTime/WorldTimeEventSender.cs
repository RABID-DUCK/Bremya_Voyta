using Photon.Pun;
using System;
using UnityEngine;

public class WorldTimeEventSender : MonoBehaviourPunCallbacks
{
    [SerializeField] private WorldTime worldTime;

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
    /// Сбросить стрелки!
    /// </summary>
    public event Action OnResetArrows = delegate { };

    /// <summary>
    /// Время спать!
    /// </summary>
    public event Action OnSleepTime = delegate { };

    /// <summary>
    /// Старт налогов!
    /// </summary>
    public event Action OnStartTaxEvent = delegate { };
    /// <summary>
    /// Стоп налогов!
    /// </summary>
    public event Action OnStopTaxEvent = delegate { };

    /// <summary>
    /// Старт менялы!
    /// </summary>
    public event Action OnStartCasinoEvent = delegate { };
    /// <summary>
    /// Стоп менялы!
    /// </summary>
    public event Action OnStopCasinoEvent = delegate { };

    /// <summary>
    /// Старт игровое событие!
    /// </summary>
    public event Action OnStartEvent = delegate { };
    /// <summary>
    /// Стоп игровое событие!
    /// </summary>
    public event Action OnStopEvent = delegate { };

    /// <summary>
    /// Конец игры!
    /// </summary>
    public event Action OnEndGame = delegate { };

    bool SI1 = false;
    bool SI2 = false;
    bool SI3 = false;
    bool SI4 = false;

    bool CASINO1 = false;
    bool CASINO2 = false;
    bool CASINO3 = false;
    bool CASINO4 = false;

    bool TAX1 = false;
    bool TAX2 = false;

    bool END = false;

    private void Update()
    {
        EventSender(worldTime.countOfDaysElapsed, worldTime.timeProgress, worldTime.isCheckTimeOfDay);
    }

    public void EventSender(int countOfDaysElapsed, float timeProgress, bool isCheckTimeOfDay)
    {
        //----------- Главные события ----------//
        OnGetTimeOfDay?.Invoke(isCheckTimeOfDay);

        OnGetNumberDay?.Invoke(countOfDaysElapsed);

        OnGetTimeProgress?.Invoke(timeProgress);
        //--------------------------------------//

        //------- Сон -------//
        if (isCheckTimeOfDay == false && countOfDaysElapsed < 5 && timeProgress >= 0f && timeProgress <= 0.01f) // МОЖЕТ НЕ РАБОТАТЬ (ПИШУ ДЛЯ КАТИ)
        {
            OnSleepTime?.Invoke();
        }
        //-------------------//

        if (PhotonNetwork.IsMasterClient)
        {
            //--------------- Стандартные(Природные и т.п.) события ---------------//
            if (countOfDaysElapsed == 1 && timeProgress > 0.6f && isCheckTimeOfDay)
            {
                if (!SI1)
                {
                    OnStartEvent?.Invoke();

                    SI1 = true;
                }
            }


            if (countOfDaysElapsed == 4 && timeProgress > 0.6f && isCheckTimeOfDay)
            {
                if (!SI2)
                {
                    OnStartEvent?.Invoke();

                    SI2 = true;
                }
            }


            if (countOfDaysElapsed == 2 && timeProgress > 0.95f && isCheckTimeOfDay)
            {
                if (!SI3)
                {
                    OnStopEvent?.Invoke();

                    SI3 = true;
                }
            }


            if (countOfDaysElapsed == 5 && timeProgress > 0.95f && isCheckTimeOfDay)
            {
                if (!SI4)
                {
                    OnStopEvent?.Invoke();

                    SI4 = true;
                }
            }
            //----------------------------------------------------------------------//
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //----------------------------- Менялы -----------------------------//
            if (isCheckTimeOfDay == true && countOfDaysElapsed == 2 && timeProgress >= 0f)
            {
                if (!CASINO1)
                {
                    OnStartCasinoEvent?.Invoke();

                    CASINO1 = true;
                }
            }


            if (isCheckTimeOfDay == true && countOfDaysElapsed == 2 && timeProgress >= 0.8f)
            {
                if (!CASINO2)
                {
                    OnStopCasinoEvent?.Invoke();

                    CASINO2 = true;
                }
            }


            if (isCheckTimeOfDay == true && countOfDaysElapsed == 5 && timeProgress >= 0f)
            {
                if (!CASINO3)
                {
                    OnStartCasinoEvent?.Invoke();

                    CASINO3 = true;
                }
            }


            if (isCheckTimeOfDay == true && countOfDaysElapsed == 5 && timeProgress >= 0.45f)
            {
                if (!CASINO4)
                {
                    OnStopCasinoEvent?.Invoke();

                    CASINO4 = true;
                }
            }
            //------------------------------------------------------------------//
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //---------------------------------- Налоги -----------------------------------//
            if (countOfDaysElapsed == 5 && timeProgress >= 0.5f && isCheckTimeOfDay == true)
            {
                if (!TAX1)
                {
                    OnStartTaxEvent?.Invoke();

                    TAX1 = true;
                }
            }


            if (countOfDaysElapsed == 5 && timeProgress >= 0.9f && isCheckTimeOfDay == true)
            {
                if (!TAX2)
                {
                    OnStopTaxEvent?.Invoke();

                    TAX2 = true;
                }
            }

            //-----------------------------------------------------------------------------// 

            //---------------------- Конец игры ---------------------//
            if (countOfDaysElapsed == 5 && isCheckTimeOfDay == false) // МОЖЕТ НЕ РАБОТАТЬ (ПИШУ ДЛЯ КАТИ)
            {
                if (!END)
                {
                    OnEndGame?.Invoke();

                    END = true;
                }
            }
            //-------------------------------------------------------//
        }
    }
}