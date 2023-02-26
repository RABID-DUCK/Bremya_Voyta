using System;
using UnityEngine;
using UnityEngine.Video;

public class SleepPresenter : SleepModel
{
    public event Action OnSkipArrow;
    public event Action OnAddDebuffToPlayer;

    [SerializeField] private WorldTimeEventSender worldTimeEventSender;

    [Header("View scripts")]
    [SerializeField] private Bed bed;
    [SerializeField] private EmergencySleepView emergencySleepView;

    [Header("Sleep settings")]
    [SerializeField] private VideoClip sleepVideo;

    private bool isTimeSleep;
    private bool isSleeping;

    private void Start()
    {
        worldTimeEventSender.OnSleepTime += IsSleepTime;

        emergencySleepView.OnTimerIsOut += StartEmergencySleep;

        bed.OnClickOnBed += StartSleep;
    }

    private void IsSleepTime()
    {
        isTimeSleep = true;

        ShowEmargencyPanel();
    }

    private void ShowEmargencyPanel()
    {
        if (isTimeSleep && isSleeping == false)
        {
            worldTimeEventSender.OnSleepTime -= ShowEmargencyPanel;

            emergencySleepView.ShowEmergencySleepPanel();
        }
    }

    private void StartEmergencySleep()
    {
        if (isTimeSleep == true && isSleeping == false)
        {
            GoSleep(sleepVideo, isSleeping);

            OnSkipArrow?.Invoke();

            OnAddDebuffToPlayer?.Invoke();
        }
    }

    private void StartSleep()
    {
        if(isTimeSleep == true)
        {
            isSleeping = true;

            emergencySleepView.HideEmergencySleepPanel();

            GoSleep(sleepVideo, isSleeping);

            isTimeSleep = false;

            emergencySleepView.OnTimerIsOut += StartEmergencySleep;

            OnSkipArrow?.Invoke();
        }
    }
}