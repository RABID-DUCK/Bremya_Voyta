using System;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [Header("Event controller settings")]
    [SerializeField] private WorldTime worldTime;

    private int randomEvent;

    private bool IsNegativeWeather = false;

    public static event Action<bool> OnGetWeather;

    public static event Action<EventSO> OnGetEventSO = delegate { };

    public static event Action OnEndEvent = delegate { };

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private Mine�ollapseEvent mine�ollapseEvent;
    [SerializeField] private StandartDayEvent standartDayEvent;
    [SerializeField] private StormEvent stormEvent;
    [SerializeField] private ThunderstormWithHeavyRainEvent ThunderstormWithHeavyRainEvent;

    private void Start()
    {
        worldTime.OnStartEvent += SelectEventByTime;
    }

    public void SelectEventByTime()
    {
        SelectRandomEvent();

        SelectEventByRandomizeNumber(randomEvent);

        worldTime.OnStartEvent -= SelectEventByTime;

        worldTime.OnStopEvent += RemoveEvents;
    }

    public void SelectRandomEvent()
    {
        randomEvent = UnityEngine.Random.Range(1, 6);
    }

    public void SelectEventByRandomizeNumber(int randomNumberEvent)
    {
        switch (randomNumberEvent)
        {
            case 1:

                if (littleRainEvent != null)
                {
                    IsNegativeWeather = true;

                    littleRainEvent.StartSmallRainEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(littleRainEvent.littleRainSO);
                }

                break;

            case 2:

                if (ThunderstormWithHeavyRainEvent != null)
                {
                    IsNegativeWeather = true;

                    ThunderstormWithHeavyRainEvent.StartThunderEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(ThunderstormWithHeavyRainEvent.ThunderSO);
                }

                break;

            case 3:

                if (stormEvent != null)
                {
                    IsNegativeWeather = true;

                    stormEvent.StartStormEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(stormEvent.StormSO);
                }

                break;

            case 4:

                if (clearWeatherWithLittleColdEvent != null)
                {
                    IsNegativeWeather = true;

                    clearWeatherWithLittleColdEvent.StartClearWeatherWithLittleCold();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(clearWeatherWithLittleColdEvent.ClearWeatherWithLittleColdSO);
                }

                break;

            case 5:

                if (mine�ollapseEvent != null)
                {
                    IsNegativeWeather = false;

                    mine�ollapseEvent.StartMine�ollapseEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(mine�ollapseEvent.Mine�ollapseSO);
                }

                break;

            case 6:

                if (standartDayEvent != null)
                {
                    IsNegativeWeather = false;

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(standartDayEvent.StandartDaySO);
                }

                break;

            default:

                Debug.Log($"������ �� �� ����� - ({randomNumberEvent})");

                break;
        }
    }

    public void RemoveEvents()
    {
        worldTime.OnStopEvent -= RemoveEvents;
        worldTime.OnStartEvent += SelectEventByTime;

        IsNegativeWeather = false;

        littleRainEvent.EndSmallRainEvent();
        ThunderstormWithHeavyRainEvent.EndThunderEvent();
        stormEvent.EndStormEvent();
        mine�ollapseEvent.EndMine�ollapseEvent();
        clearWeatherWithLittleColdEvent.EndClearWeatherWithLittleCold();

        OnEndEvent?.Invoke();
    }
}