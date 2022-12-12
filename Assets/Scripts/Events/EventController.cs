using System;
using System.Collections.Generic;
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

    private bool isNegativeWeather = false;

    private int originalDay;
    private float originalTimeInSeconds;

    public static event Action<bool> GetNegativeWeather;

    private event Action<int, float> CheckRemoveEvents;

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private Mine—ollapseEvent mine—ollapseEvent;
    [SerializeField] private StandartDayEvent standartDayEvent;
    [SerializeField] private StormEvent stormEvent;
    [SerializeField] private ThunderstormWithHeavyRainEvent ThunderstormWithHeavyRainEvent;

    private void Start()
    {
        GetNegativeWeather?.Invoke(isNegativeWeather);

        WorldTime.GetNumberDay += SetDay;
        WorldTime.GetTimeProgress += SetTimeInSeconds;

        CheckRemoveEvents?.Invoke(originalDay, originalTimeInSeconds);
        //CheckRemoveEvents += RemoveEvent;
        //CheckRemoveEvents += SelectEventByTime;
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

        List<GameObject> dfdf = new List<GameObject>();

        int panelID = 0;

        for (int i = 0; i < dfdf.Count; i++)
        {
            if(i == panelID)
            {
                dfdf[i].SetActive(true);
            }
            else
            {
                dfdf[i].SetActive(false);
            }
        }
    }

    public void SelectEventByTime(/*int day, float timeInSeconds*/)
    {
        if(worldTime.countOfDaysElapsed == firstNumberDay &&
            WorldTime.timeProgress == firstGameProgress ||
            worldTime.countOfDaysElapsed == secondNumberDay &&
            WorldTime.timeProgress == firstGameProgress)
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

                if (mine—ollapseEvent != null)
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

    public void RemoveEvent(/*int numDay, float time*/)
    {
        if(worldTime.countOfDaysElapsed == firstNumberDay + 1 && WorldTime.timeProgress == firstGameProgress ||
            worldTime.countOfDaysElapsed == secondNumberDay + 1 && WorldTime.timeProgress == secondGameProgress)
        {
            isNegativeWeather = false;

            littleRainEvent.EndSmallRain();
            ThunderstormWithHeavyRainEvent.EndThunder();
            //ƒÓÍËÌÛÚ¸ ÏÂÚÓ‰˚, ÍÓÚÓ˚Â Á‡‚Â¯‡˛Ú Ë‚ÂÌÚ˚
        }
    }
}