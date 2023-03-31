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
        if (isCheckTimeOfDay == false && countOfDaysElapsed < 5 && timeProgress >= 0f && timeProgress <= 0.001f) // МОЖЕТ НЕ РАБОТАТЬ (ПИШУ ДЛЯ КАТИ)
        {
            OnSleepTime?.Invoke();
        }
        //-------------------//

        if (PhotonNetwork.IsMasterClient)
        {
            //--------------- Стандартные(Природные и т.п.) события ---------------//
            bool SI1 = false;
            bool SI2 = false;
            bool SI3 = false;
            bool SI4 = false;

            while (!SI1)
            {
                if (countOfDaysElapsed == 1 && timeProgress > 0.6f && isCheckTimeOfDay)
                {
                    OnStartEvent?.Invoke();

                    SI1 = true;
                }
            }

            while (!SI2)
            {
                if (countOfDaysElapsed == 4 && timeProgress > 0.6f && isCheckTimeOfDay)
                {
                    OnStartEvent?.Invoke();

                    SI2 = true;
                }
            }

            while (!SI3)
            {
                if (countOfDaysElapsed == 2 && timeProgress > 0.95f && isCheckTimeOfDay)
                {
                    OnStopEvent?.Invoke();

                    SI3 = true;
                }
            }

            while (!SI4)
            {
                if (countOfDaysElapsed == 5 && timeProgress > 0.95f && isCheckTimeOfDay)
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
            bool CASINO1 = false;
            bool CASINO2 = false;
            bool CASINO3 = false;
            bool CASINO4 = false;

            while (!CASINO1)
            {
                if (isCheckTimeOfDay == true && countOfDaysElapsed == 2 && timeProgress >= 0f)
                {
                    OnStartCasinoEvent?.Invoke();

                    CASINO1 = true;
                }
            }

            while (!CASINO2)
            {
                if (isCheckTimeOfDay == true && countOfDaysElapsed == 2 && timeProgress >= 0.8f)
                {
                    OnStopCasinoEvent?.Invoke();

                    CASINO2 = true;
                }
            }

            while (!CASINO3)
            {
                if (isCheckTimeOfDay == true && countOfDaysElapsed == 5 && timeProgress >= 0f)
                {
                    OnStartCasinoEvent?.Invoke();

                    CASINO3 = true;
                }
            }

            while (!CASINO4)
            {
                if (isCheckTimeOfDay == true && countOfDaysElapsed == 5 && timeProgress >= 0.45f)
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
            bool TAX1 = false;
            bool TAX2 = false;

            while (!TAX1)
            {
                if (countOfDaysElapsed == 5 && timeProgress > 0.5f && isCheckTimeOfDay == true)
                {
                    OnStartTaxEvent?.Invoke();

                    TAX1 = true;
                }
            }

            while (!TAX2)
            {
                if (countOfDaysElapsed == 5 && timeProgress > 0.9f && isCheckTimeOfDay == true)
                {
                    OnStopTaxEvent?.Invoke();

                    TAX2 = true;
                }
            }
            
            //-----------------------------------------------------------------------------// 

            //---------------------- Конец игры ---------------------//
            if (countOfDaysElapsed == 5 && isCheckTimeOfDay == false) // МОЖЕТ НЕ РАБОТАТЬ (ПИШУ ДЛЯ КАТИ)
            {
                OnEndGame?.Invoke();
            }
            //-------------------------------------------------------//
        }
    }
}