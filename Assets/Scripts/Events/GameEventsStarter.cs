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

    public static event Action<EventSO> OnGetEventSO = delegate { };

    public static event Action OnEndEvent = delegate { };

    public List<AudioClip> audioClips = new List<AudioClip>(3);

    public AudioSource iventsSounds;

    [Header("Events")]
    [SerializeField] private LittleRainEvent littleRainEvent;
    [SerializeField] private ClearWeatherWithLittleColdEvent clearWeatherWithLittleColdEvent;
    [SerializeField] private MineÑollapseEvent mineÑollapseEvent;
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
                    littleRainEvent.StartSmallRainEvent();

                    OnGetEventSO?.Invoke(littleRainEvent.littleRainSO);

                    iventsSounds.clip = audioClips[0];
                }

                break;

            case 2:

                if (ThunderstormWithHeavyRainEvent != null)
                {
                    ThunderstormWithHeavyRainEvent.StartThunderEvent();

                    OnGetEventSO?.Invoke(ThunderstormWithHeavyRainEvent.ThunderSO);

                    iventsSounds.clip = audioClips[0];
                    //Debug.Log(audioClips); // Äåáàãè íóæíî êîììèòèòü èëè óáèğàòü ïîñëå òåñòèğîâàíèÿ!
                }

                break;

            case 3:

                if (stormEvent != null)
                {
                    stormEvent.StartStormEvent();

                    OnGetEventSO?.Invoke(stormEvent.StormSO);

                    iventsSounds.clip = audioClips[1];
                    //Debug.Log(audioClips);
                }

                break;

            case 4:

                if (clearWeatherWithLittleColdEvent != null)
                {
                    clearWeatherWithLittleColdEvent.StartClearWeatherWithLittleCold();

                    OnGetEventSO?.Invoke(clearWeatherWithLittleColdEvent.ClearWeatherWithLittleColdSO);
                }

                break;

            case 5:

                if (mineÑollapseEvent != null)
                {
                    mineÑollapseEvent.StartMineÑollapseEvent();

                    OnGetEventSO?.Invoke(mineÑollapseEvent.MineÑollapseSO);
                }

                break;

            case 6:

                if (standartDayEvent != null)
                {
                    OnGetEventSO?.Invoke(standartDayEvent.StandartDaySO);
                }

                break;

            default:

                Debug.LogError($"Âûïàëî íå òî ÷èñëî - ({randomNumberEvent})");

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

        littleRainEvent.EndSmallRainEvent();
        ThunderstormWithHeavyRainEvent.EndThunderEvent();
        stormEvent.EndStormEvent();
        mineÑollapseEvent.EndMineÑollapseEvent();
        clearWeatherWithLittleColdEvent.EndClearWeatherWithLittleCold();

        //iventsSounds.Stop(); // Çäåñü êèäàåò îøèáêó. Âìåñòî îáíóëåíèÿ ìîæíî ïğîñòî îñòàíàâëèâàòü ïğîèãğûâàòåëü, à íóæíûé çâóê òû óæå ïğîêèäûâàåøü âî âğåìÿ ïîäêëş÷åíèÿ âûïàâøåãî èâåíòà.

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
            if ((string)propertiesThatChanged["EndEvent"] == "Clear")
            {
                ClearWeather();
            }
        }
    }

    private void OnDestroy()
    {
        worldTimeEventSender.OnStartEvent -= SelectEventByTime;
    }
}