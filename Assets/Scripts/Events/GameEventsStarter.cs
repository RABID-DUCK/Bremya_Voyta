using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsStarter : MonoBehaviourPunCallbacks
{
    [Header("Event controller settings")]
    [SerializeField] private WorldTimeEventSender worldTimeEventSender;

    private int randomEvent;

    private bool IsNegativeWeather = false;

    public static event Action<bool> OnGetWeather;

    public static event Action<EventSO> OnGetEventSO = delegate { };

    public static event Action OnEndEvent = delegate { };

    public List<AudioClip> audioClips = new List<AudioClip>(3);

    public AudioSource iventsSounds;

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private Mine�ollapseEvent mine�ollapseEvent;
    [SerializeField] private StandartDayEvent standartDayEvent;
    [SerializeField] private StormEvent stormEvent;
    [SerializeField] private ThunderstormWithHeavyRainEvent ThunderstormWithHeavyRainEvent;

    private void Start()
    {
        worldTimeEventSender.OnStartEvent += SelectEventByTime;
    }

    // [ContextMenu("Add Random Event")]
    public void SelectEventByTime()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            randomEvent = SelectRandomEvent();
            Hashtable _CP = new Hashtable() { { "StartEvent", randomEvent } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);
        }
    }

    public int SelectRandomEvent()
    {
        return UnityEngine.Random.Range(1, 6);
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

                    OnGetEventSO?.Invoke(littleRainEvent.littleRainSO);

                    iventsSounds.clip = audioClips[0];
                }

                break;

            case 2:

                if (ThunderstormWithHeavyRainEvent != null)
                {
                    IsNegativeWeather = true;

                    ThunderstormWithHeavyRainEvent.StartThunderEvent();

                    OnGetEventSO?.Invoke(ThunderstormWithHeavyRainEvent.ThunderSO);

                    iventsSounds.clip = audioClips[0];
                    //Debug.Log(audioClips); // ������ ����� ��������� ��� ������� ����� ������������!
                }

                break;

            case 3:

                if (stormEvent != null)
                {
                    IsNegativeWeather = true;

                    stormEvent.StartStormEvent();

                    OnGetEventSO?.Invoke(stormEvent.StormSO);

                    iventsSounds.clip = audioClips[1];
                    //Debug.Log(audioClips);
                }

                break;

            case 4:

                if (clearWeatherWithLittleColdEvent != null)
                {
                    IsNegativeWeather = true;

                    clearWeatherWithLittleColdEvent.StartClearWeatherWithLittleCold();

                    OnGetEventSO?.Invoke(clearWeatherWithLittleColdEvent.ClearWeatherWithLittleColdSO);
                }

                break;

            case 5:

                if (mine�ollapseEvent != null)
                {
                    IsNegativeWeather = false;

                    mine�ollapseEvent.StartMine�ollapseEvent();

                    OnGetEventSO?.Invoke(mine�ollapseEvent.Mine�ollapseSO);
                }

                break;

            case 6:

                if (standartDayEvent != null)
                {
                    IsNegativeWeather = false;

                    OnGetEventSO?.Invoke(standartDayEvent.StandartDaySO);
                }

                break;

            default:

                Debug.LogError($"������ �� �� ����� - ({randomNumberEvent})");

                break;
        }
    }

    // [ContextMenu("Clear Events")]
    public void RemoveEvents()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "EndEvent", "Clear" } });
        }
    }

    private void ClearWeather()
    {
        worldTimeEventSender.OnStopEvent -= RemoveEvents;
        worldTimeEventSender.OnStartEvent += SelectEventByTime;

        IsNegativeWeather = false;

        littleRainEvent.EndSmallRainEvent();
        ThunderstormWithHeavyRainEvent.EndThunderEvent();
        stormEvent.EndStormEvent();
        mine�ollapseEvent.EndMine�ollapseEvent();
        clearWeatherWithLittleColdEvent.EndClearWeatherWithLittleCold();

        //iventsSounds.Stop(); // ����� ������ ������. ������ ��������� ����� ������ ������������� �������������, � ������ ���� �� ��� ������������ �� ����� ����������� ��������� ������.

        OnEndEvent?.Invoke();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("StartEvent"))
        {
            SelectEventByRandomizeNumber((int)PhotonNetwork.CurrentRoom.CustomProperties["StartEvent"]);

            worldTimeEventSender.OnStartEvent -= SelectEventByTime;
            worldTimeEventSender.OnStopEvent += RemoveEvents;
        }

        if (propertiesThatChanged.ContainsKey("EndEvent"))
        {
            ClearWeather();
        }
    }

    private void OnDestroy()
    {
        worldTimeEventSender.OnStartEvent -= SelectEventByTime;
    }
}