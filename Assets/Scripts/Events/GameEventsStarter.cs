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

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private Mine—ollapseEvent mine—ollapseEvent;
    [SerializeField] private StandartDayEvent standartDayEvent;
    [SerializeField] private StormEvent stormEvent;
    [SerializeField] private ThunderstormWithHeavyRainEvent ThunderstormWithHeavyRainEvent;

    private void Start()
    {
        worldTimeEventSender.OnStartEvent += SelectEventByTime;
    }

    [ContextMenu("StartEvent")]
    public void StartEventik()
    {
        SelectEventByTime();
    }

    public void SelectEventByTime()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            randomEvent = SelectRandomEvent();
            Hashtable _CP = new Hashtable() { { "StartEvent", randomEvent } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);

            worldTimeEventSender.OnStartEvent -= SelectEventByTime;
            worldTimeEventSender.OnStopEvent += RemoveEvents;
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

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(littleRainEvent.littleRainSO);

                    SoundManager.Instance.PlaySound(audioClips[0]);
                }

                break;

            case 2:

                if (ThunderstormWithHeavyRainEvent != null)
                {
                    IsNegativeWeather = true;

                    ThunderstormWithHeavyRainEvent.StartThunderEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(ThunderstormWithHeavyRainEvent.ThunderSO);

                    SoundManager.Instance.PlaySound(audioClips[0]);
                    //Debug.Log(audioClips); // ƒÂ·‡„Ë ÌÛÊÌÓ ÍÓÏÏËÚËÚ¸ ËÎË Û·Ë‡Ú¸ ÔÓÒÎÂ ÚÂÒÚËÓ‚‡ÌËˇ!
                }

                break;

            case 3:

                if (stormEvent != null)
                {
                    IsNegativeWeather = true;

                    stormEvent.StartStormEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(stormEvent.StormSO);

                    SoundManager.Instance.PlaySound(audioClips[1]);
                    //Debug.Log(audioClips);
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

                if (mine—ollapseEvent != null)
                {
                    IsNegativeWeather = false;

                    mine—ollapseEvent.StartMine—ollapseEvent();

                    OnGetWeather?.Invoke(IsNegativeWeather);

                    OnGetEventSO?.Invoke(mine—ollapseEvent.Mine—ollapseSO);
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

                Debug.LogError($"¬˚Ô‡ÎÓ ÌÂ ÚÓ ˜ËÒÎÓ - ({randomNumberEvent})");

                break;
        }
    }

    public void RemoveEvents()
    {
        worldTimeEventSender.OnStopEvent -= RemoveEvents;
        worldTimeEventSender.OnStartEvent += SelectEventByTime;

        IsNegativeWeather = false;

        littleRainEvent.EndSmallRainEvent();
        ThunderstormWithHeavyRainEvent.EndThunderEvent();
        stormEvent.EndStormEvent();
        mine—ollapseEvent.EndMine—ollapseEvent();
        clearWeatherWithLittleColdEvent.EndClearWeatherWithLittleCold();

        SoundManager.Instance.StopSound(audioClips);
        
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
    }

    private void OnDestroy()
    {
        worldTimeEventSender.OnStartEvent -= SelectEventByTime;
    }
}