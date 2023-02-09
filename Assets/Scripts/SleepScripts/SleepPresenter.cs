using System;
using UnityEngine;
using UnityEngine.Video;

public class SleepPresenter : SleepModel
{
    [Header("View scripts")]
    //[SerializeField] private EmergencySleepView emergencySleepView;
    [SerializeField] private Bed bed;

    [Header("Sleep settings")]
    [SerializeField] private VideoClip sleepVideo;

    private bool isICanSleep = false;

    public event Action OnSkipArrow = delegate { };

    //private bool isSleeping = false;

    //public event Action<bool> OnIsSleeping = delegate { };

    private void Start()
    {
        worldTime.IsSleepTime += AllowToSleep;

        //emergencySleepView.OnTimerIsOut += GoToFastSleep;

        bed.OnClickOnBed += CheckingIfICanSleep;
    }

    private void AllowToSleep()
    {
        //worldTime.IsSleepTime -= AllowToSleep;

        //emergencySleepView.ShowEmergencySleepPanel();

        isICanSleep = true;
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
        isICanSleep = false;

        //emergencySleepView.HideEmergencySleepPanel();

        GoSleep(sleepVideo);

        OnSkipArrow?.Invoke();
    }

    //private void GoToFastSleep()
    //{
    //    emergencySleepView.OnTimerIsOut -= GoToFastSleep;

    //    isICanSleep = false;

    //    GoSleep(sleepVideo);
    //}
}