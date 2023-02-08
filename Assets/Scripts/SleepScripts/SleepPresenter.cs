using System;
using UnityEngine;
using UnityEngine.Video;

public class SleepPresenter : SleepModel
{
    [Header("View scripts")]
    [SerializeField] private EmergencySleepView emergencySleepView;
    [SerializeField] private Bed bed;

    [Header("Sleep settings")]
    [SerializeField] private VideoClip sleepVideo;

    [SerializeField] private WorldTime worldTime;

    private bool isICanSleep = false;

    private bool isSleeping = false;

    public event Action<bool> OnIsSleeping = delegate { };

    private void Start()
    {
        worldTime.IsSleepTime += AllowToSleep;

        bed.OnClickOnBed += CheckingIfICanSleep;

        emergencySleepView.OnTimerIsOut += GoFastSleep;
    }

    private void AllowToSleep()
    {
        worldTime.IsSleepTime -= AllowToSleep;

        isICanSleep = true;

        StartEmergencySleepView();
    }

    private void StartEmergencySleepView()
    {
        emergencySleepView.ShowEmergencySleepPanel();
        emergencySleepView.StartTimerEmergencySleepPanel();
    }

    private void CheckingIfICanSleep()
    {
        if (isICanSleep)
        {
            UIController.ShowYesNoDialog("Вы хотите лечь спать?", GoToSleep);
        }
        else
        {
            UIController.ShowInfo("Вы не можете спать днем!", "Ок");
        }
    }

    private void GoToSleep()
    {
        GoSleep(sleepVideo);

        isSleeping = true;

        OnIsSleeping?.Invoke(isSleeping);

        AfterSleep(worldTime, isICanSleep, isSleeping);

        emergencySleepView.HideEmergencySleepPanel();

        worldTime.IsSleepTime += AllowToSleep;
    }

    private void GoFastSleep()
    {
        if(isSleeping == false)
        {
            GoSleep(sleepVideo);

            AfterSleep(worldTime, isICanSleep, isSleeping);

            OnIsSleeping?.Invoke(false);

            worldTime.IsSleepTime += AllowToSleep;
        }
    }
}