using Photon.Pun;
using System;
using Unity.VisualScripting;
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

        RoundingValues(timeProgress, out timeProgress); // Диапазон будет сокращен до 10 000-х знаков после запятой

        //Debug.Log(timeProgress); // Если нужно отследить, какие значения выходят

        //------- Сон -------//
        if (isCheckTimeOfDay == false && countOfDaysElapsed < 5 && timeProgress >= 0f && timeProgress <= 0.01f)
        {
            OnSleepTime?.Invoke();
        }
        //-------------------//

        if (PhotonNetwork.IsMasterClient)
        {
            //--------------- Стандартные(Природные и т.п.) события ---------------//
            if (countOfDaysElapsed == 1 && timeProgress >= 0.6f && timeProgress <= 0.61f && isCheckTimeOfDay)
            {
                OnStartEvent?.Invoke();
            }

            if (countOfDaysElapsed == 4 && timeProgress > 0.6f && timeProgress <= 0.61f && isCheckTimeOfDay)
            {
                OnStartEvent?.Invoke();
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
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //----------------------------- Менялы -----------------------------//
            if (isCheckTimeOfDay == true && countOfDaysElapsed == 2 && timeProgress >= 0f && timeProgress <= 0.001f)
            {
                OnStartCasinoEvent?.Invoke();
            }

            if (isCheckTimeOfDay == true && countOfDaysElapsed == 2 && timeProgress >= 0.8f && timeProgress <= 0.81f)
            {
                OnStopCasinoEvent?.Invoke();
            }

            if (isCheckTimeOfDay == true && countOfDaysElapsed == 5 && timeProgress >= 0f && timeProgress <= 0.001f)
            {
                OnStartCasinoEvent?.Invoke();
            }

            if (isCheckTimeOfDay == true && countOfDaysElapsed == 5 && timeProgress >= 0.45f && timeProgress <= 0.46f)
            {
                OnStopCasinoEvent?.Invoke();
            }
            //------------------------------------------------------------------//
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //---------------------------------- Налоги -----------------------------------//
            if (countOfDaysElapsed == 5 && timeProgress > 0.5f && timeProgress <= 0.51f && isCheckTimeOfDay == true)
            {
                OnStartTaxEvent?.Invoke();
            }

            if (countOfDaysElapsed == 5 && timeProgress > 0.9f && timeProgress < 0.91f && isCheckTimeOfDay == true)
            {
                OnStopTaxEvent?.Invoke();
            }
            //-----------------------------------------------------------------------------// 

            //---------------------- Конец игры ---------------------// -_-
            if (countOfDaysElapsed == 5 && isCheckTimeOfDay == false)
            {
                OnEndGame?.Invoke();
            }
            //-------------------------------------------------------//
        }
    }

    private void RoundingValues(float timeProgress, out float secondTimeProgress)
    {
        secondTimeProgress = (float)Math.Round(timeProgress, 4); // Округляет до 4-х знаков после запятой
    }
}