using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EmergencySleepView : MonoBehaviour
{
    public event Action OnTimerIsOut;

    [Header("Panel settings")]
    [Tooltip("A panel that counts down 15 seconds to sleep")]
    [SerializeField] private GameObject emergencyPanelSleep;
    [SerializeField] private TMP_Text timerText;

    [Space, SerializeField] private ShowCanvasGroup ShowCanvasGroup;


    public void StartTimerEmergencySleepPanel()
    {
        StartCoroutine(StartTimer(timerText));
    }

    public void ShowEmergencySleepPanel()
    {
        ShowCanvasGroup.Show();

        StartTimerEmergencySleepPanel();
    }

    public void HideEmergencySleepPanel()
    {
        ShowCanvasGroup.Hide();

        StopCoroutine(StartTimer(timerText));
    }

    private IEnumerator StartTimer(TMP_Text timerText)
    {
        for (int i = 15; i > 0; i--)
        {
            if (i > 4)
            {
                timerText.text = $"Вам нужно лечь спать!\r\n" +
                                $"Вы уснете, через {i} секунд!";
            }
            else if (i > 1 && i < 5)
            {
                timerText.text = $"Вам нужно лечь спать!\r\n" +
                                $"Вы уснете, через {i} секунды!";
            }
            else if (i == 1)
            {
                timerText.text = $"Вам нужно лечь спать!\r\n" +
                                $"Вы уснете, через {i} секунду!";
            }

            yield return new WaitForSeconds(1f);

        }

        OnTimerIsOut?.Invoke();

        HideEmergencySleepPanel();
    }
}