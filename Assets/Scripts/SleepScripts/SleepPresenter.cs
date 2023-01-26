using System;
using UnityEngine;
using UnityEngine.Video;

public class SleepPresenter : SleepModel
{
    [Header("View scripts")]
    [SerializeField] private SleepView sleepView;

    [Header("Sleep settings")]
    [SerializeField] private VideoClip sleepVideo;

    private EmergencySleepView emergencySleepView;
    private WorldTime worldTime;

    public bool isSleeping { get; private set; }

    public event Action<bool> OnSleeping = delegate { };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        worldTime = FindObjectOfType<WorldTime>();

        emergencySleepView = FindObjectOfType<EmergencySleepView>();
    }

    private void Start()
    {
        worldTime.GetTimeOfDay += StartEnergenceSleepPanel;

        emergencySleepView.OnTimerIsOut += GoFastSleep;

        if (sleepView != null)
        {
            sleepView.OnClickYesButton += GoSleep;
        }
    }

    private void StartEnergenceSleepPanel(bool checkTimeOfDay)
    {
        if (checkTimeOfDay == false)
        {
            ResetIsSleeping(isSleeping);

            emergencySleepView.ShowEmergencySleepPanel();
            emergencySleepView.StartTimerEmergencySleepPanel();
        }
    }

    private void GoFastSleep()
    {
        if(isSleeping == false)
        {
            GoSleep(sleepVideo);
            AfterSleep(worldTime);

            OnSleeping?.Invoke(isSleeping);
        }
    }

    private void GoSleep()
    {
        GoSleep(sleepVideo);
        AfterSleep(worldTime);

        isSleeping = true;

        OnSleeping?.Invoke(isSleeping);
    }
}