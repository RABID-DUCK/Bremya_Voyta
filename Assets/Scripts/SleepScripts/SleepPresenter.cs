using System;
using UnityEngine;

public class SleepPresenter : MonoBehaviour
{
    [Header("View scripts")]
    [SerializeField] private SleepView sleepView;

    private EmergencySleepView emergencySleepView;

    [Header("Model script")]
    [SerializeField] private SleepModel sleepModel;

    private WorldTime worldTime;

    public bool isSleeping { get; private set; }

    public event Action<bool> OnSleeping = delegate { };

    private void Awake()
    {
        worldTime = FindObjectOfType<WorldTime>();

        emergencySleepView = FindObjectOfType<EmergencySleepView>();
    }

    private void Start()
    {
        worldTime.GetTimeOfDay += ShowEnergenceSleepPanel;

        emergencySleepView.OnTimerIsOut += GoFastSleep;

        sleepView.OnClickYesButton += GoSleep;

        sleepModel.OnEndSleep += AfterSleep;
    }

    private void ShowEnergenceSleepPanel(bool checkTimeOfDay)
    {
        if (checkTimeOfDay == false)
        {
            sleepModel.ResetIsSleeping(isSleeping);

            emergencySleepView.ShowEmergencySleepPanel();
            emergencySleepView.StartTimerEmergencySleepPanel();
        }
    }

    private void GoFastSleep()
    {
        if(isSleeping == false)
        {
            sleepModel.GoSleep();

            OnSleeping?.Invoke(isSleeping);
        }
    }

    private void GoSleep()
    {
        sleepModel.GoSleep();

        isSleeping = true;

        OnSleeping?.Invoke(isSleeping);
    }

    private void AfterSleep()
    {
        sleepModel.AfterSleep(worldTime);
    }
}