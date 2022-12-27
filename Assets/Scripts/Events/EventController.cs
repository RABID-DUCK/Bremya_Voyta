using System;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [Header("Event controller settings")]
    [SerializeField] private WorldTime worldTime;

    [Space, Tooltip("First half of the week. The day the event is to take place")]
    public int firstNumberDay;
    [Tooltip("First half of the week. Time when the event should take place")]
    public float firstGameProgress;

    [Space, Tooltip("Second half of the week. The day the event is to take place")]
    public int secondNumberDay;
    [Tooltip("Second half of the week. Time when the event should take place")]
    public float secondGameProgress;

    private int randomEvent;

    private bool IsNegativeWeather = false;

    private int originalDay;
    private float originalTimeInSeconds;

    public static event Action<bool> OnGetWeather;

    public static event Action<EventSO> OnGetEventSO = delegate { };

    public static event Action OnEndEvent = delegate { };

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private Mine—ollapseEvent mine—ollapseEvent;
    [SerializeField] private StandartDayEvent standartDayEvent;
    [SerializeField] private StormEvent stormEvent;
    [SerializeField] private ThunderstormWithHeavyRainEvent ThunderstormWithHeavyRainEvent;

    private void Start()
    {
        WorldTime.GetNumberDay += SetDay;
        WorldTime.GetTimeProgress += SetTimeInSeconds;

        randomEvent = 6;
    }

    public void SetDay(int day)
    {
        originalDay = day;
    }

    public void SetTimeInSeconds(float timeInSeconds)
    {
        originalTimeInSeconds = timeInSeconds;
    }

    public void Update()
    {
        SelectEventByTime();

        RemoveEvent();
    }

    public void SelectEventByTime()
    {
        if (WorldTime.IsStartTime)
        {
            if (worldTime.countOfDaysElapsed == firstNumberDay &&
            WorldTime.timeProgress == firstGameProgress ||
            worldTime.countOfDaysElapsed == secondNumberDay &&
            WorldTime.timeProgress == firstGameProgress)
            {
                SelectRandomEvent();

                SelectEventByRandomizeNumber(randomEvent);
            }
        }
    }

    public void SelectRandomEvent()
    {
        randomEvent = UnityEngine.Random.Range(0, 5);
    }

    public void SelectEventByRandomizeNumber(int randomNumberEvent)
    {
        switch (randomNumberEvent)
        {
            case 0:

                if (littleRainEvent != null)
                {
                    IsNegativeWeather = true;

                    littleRainEvent.StartSmallRainEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(littleRainEvent.littleRainSO);
                }

                break;

            case 1:

                if (ThunderstormWithHeavyRainEvent != null)
                {
                    IsNegativeWeather = true;

                    ThunderstormWithHeavyRainEvent.StartThunderEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(ThunderstormWithHeavyRainEvent.ThunderSO);
                }

                break;

            case 2:

                if (stormEvent != null)
                {
                    IsNegativeWeather = true;

                    stormEvent.StartStormEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(stormEvent.StormSO);
                }

                break;

            case 3:

                if (clearWeatherWithLittleColdEvent != null)
                {
                    IsNegativeWeather = true;

                    clearWeatherWithLittleColdEvent.StartClearWeatherWithLittleCold();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(clearWeatherWithLittleColdEvent.ClearWeatherWithLittleColdSO);
                }

                break;

            case 4:

                if (mine—ollapseEvent != null)
                {
                    IsNegativeWeather = false;

                    mine—ollapseEvent.StartMine—ollapseEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(mine—ollapseEvent.Mine—ollapseSO);
                }

                break;

            case 5:

                if (standartDayEvent != null)
                {
                    IsNegativeWeather = false;

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(standartDayEvent.StandartDaySO);
                }

                break;
        }
    }

    public void RemoveEvent()
    {
        if (worldTime.countOfDaysElapsed == firstNumberDay + 1 && WorldTime.timeProgress == firstGameProgress ||
            worldTime.countOfDaysElapsed == secondNumberDay + 1 && WorldTime.timeProgress == secondGameProgress)
        {
            IsNegativeWeather = false;

            littleRainEvent.EndSmallRainEvent();
            ThunderstormWithHeavyRainEvent.EndThunderEvent();
            stormEvent.EndStormEvent();
            mine—ollapseEvent.EndMine—ollapseEvent();
            clearWeatherWithLittleColdEvent.EndClearWeatherWithLittleCold();

            OnEndEvent?.Invoke();
        }
    }
}