using UnityEngine;

public class EventController : MonoBehaviour
{
    [Header("Event controller settings")]
    [SerializeField] private WorldTime worldTime;

    public int firstNumberDay;
    public int firstTimeForSelectEvent;

    public int secondNumberDay;
    public int secondTimeForSelectEvent;

    private int randomEvent;

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private Mine—ollapseEvent mine—ollapseEvent;
    [SerializeField] private StandartDayEvent standartDayEvent;
    [SerializeField] private StormEvent stormEvent;
    [SerializeField] private ThunderstormWithHeavyRainEvent ThunderstormWithHeavyRainEvent;

    public void SelectEventByTime()
    {
        if(worldTime.countOfDaysElapsed == firstNumberDay &&
            worldTime.timeDayInSeconds == firstTimeForSelectEvent)
        {
            SelectRandomEvent();

            SelectEventByRandomizeNumber(randomEvent);
        }

        if(worldTime.countOfDaysElapsed == secondNumberDay &&
            worldTime.timeDayInSeconds == firstTimeForSelectEvent)
        {
            SelectRandomEvent();

            SelectEventByRandomizeNumber(randomEvent);
        }
    }

    public void SelectRandomEvent()
    {
        randomEvent = 0;

        randomEvent = Random.Range(0, 5);
    }

    public void SelectEventByRandomizeNumber(int randomNumberEvent)
    {
        switch (randomNumberEvent)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
        }
    }

    public void RemoveEvent()
    {

    }
}