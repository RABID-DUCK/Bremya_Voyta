using Photon.Pun.Demo.Cockpit;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [Header("Event controller settings")]
    [SerializeField] private WorldTime worldTime;

    [Space, Tooltip("First half of the week. The day the event is to take place")]
    public int firstNumberDay;
    [Tooltip("First half of the week. Time when the event should take place")]
    public float firstTimeForSelectEvent;

    [Space, Tooltip("Second half of the week. The day the event is to take place")]
    public int secondNumberDay;
    [Tooltip("Second half of the week. Time when the event should take place")]
    public float secondTimeForSelectEvent;

    private int randomEvent;

    private bool isNegativeWeather = false;

    private int currentDay;
    private float currentTimeInSeconds;

    public static event Action<bool> GetNegativeWeather;

    private event Action<int, float> CheckRemoveEvents;

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private MineÑollapseEvent mineÑollapseEvent;
    [SerializeField] private StandartDayEvent standartDayEvent;
    [SerializeField] private StormEvent stormEvent;
    [SerializeField] private ThunderstormWithHeavyRainEvent ThunderstormWithHeavyRainEvent;

    private void Start()
    {
        GetNegativeWeather?.Invoke(isNegativeWeather);

        WorldTime.getNumberDay += SetDay;
        WorldTime.getTimeInSeconds += SetTimeInSeconds;

        CheckRemoveEvents?.Invoke(currentDay, currentTimeInSeconds);
        CheckRemoveEvents += RemoveEvent;
        CheckRemoveEvents += SelectEventByTime;
    }

    public void SetDay(int day)
    {
        currentDay = day;
    }

    public void SetTimeInSeconds(float timeInSeconds)
    {
        currentTimeInSeconds = timeInSeconds;
    }

    public void SelectEventByTime(int day, float timeInSeconds)
    {
        if(day == firstNumberDay &&
            timeInSeconds == firstTimeForSelectEvent ||
            day == secondNumberDay &&
            timeInSeconds == firstTimeForSelectEvent)
        {
            SelectRandomEvent();

            SelectEventByRandomizeNumber(randomEvent);
        }
    }

    public void SelectRandomEvent()
    {
        randomEvent = 0;

        randomEvent = UnityEngine.Random.Range(0, 5);
    }

    public void SelectEventByRandomizeNumber(int randomNumberEvent)
    {
        switch (randomNumberEvent)
        {
            case 0:

                if(littleRainEvent != null)
                {
                    isNegativeWeather = true;

                    littleRainEvent.StartSmallRain();
                }

                break;
            case 1:

                if (ThunderstormWithHeavyRainEvent != null)
                {
                    isNegativeWeather = true;

                    ThunderstormWithHeavyRainEvent.StartThunder();
                }

                break;
            case 2:

                if (stormEvent != null)
                {
                    isNegativeWeather = true;


                }

                break;
            case 3:

                if (clearWeatherWithLittleColdEvent != null)
                {

                }

                break;
            case 4:

                if (mineÑollapseEvent != null)
                {

                }

                break;
            case 5:

                if (standartDayEvent != null)
                {

                }

                break;
        }
    }

    public void RemoveEvent(int numDay, float time)
    {
        if(numDay == firstNumberDay + 1 && time == 260 ||
            numDay == secondNumberDay + 1 && time == 260)
        {
            isNegativeWeather = false;

            littleRainEvent.EndSmallRain();
            ThunderstormWithHeavyRainEvent.EndThunder();
            //Äîêèíóòü ìåòîäû, êîòîðûå çàâåðøàþò èâåíòû
        }
    }
}