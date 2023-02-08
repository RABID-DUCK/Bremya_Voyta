using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EmergencySleepView : MonoBehaviour
{
    [Header("UI panels")]
    [Tooltip("A panel that counts down 15 seconds to sleep")]
    [SerializeField] private GameObject emergencyPanelSleep;
    [SerializeField] private TMP_Text timerText;

    [Space, SerializeField] private ShowCanvasGroup ShowCanvasGroup;

    public event Action OnTimerIsOut;

    public void StartTimerEmergencySleepPanel()
    {
        StartCoroutine(StartTimer(timerText.text));

        OnTimerIsOut?.Invoke();
    }

    public void ShowEmergencySleepPanel()
    {
        ShowCanvasGroup.Show();
    }

    public void HideEmergencySleepPanel()
    {
        ShowCanvasGroup.Hide();

        StopCoroutine(StartTimer(timerText.text));
    }

    private IEnumerator StartTimer(string timerText)
    {
        for (int i = 15; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            timerText = i.ToString();
        }

        HideEmergencySleepPanel();
    }
}