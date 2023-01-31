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

    public event Action OnTimerIsOut;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartTimerEmergencySleepPanel()
    {
        StartCoroutine(StartTimer(timerText.text));

        OnTimerIsOut?.Invoke();
    }

    public void ShowEmergencySleepPanel()
    {
        emergencyPanelSleep.SetActive(true);
    }

    public void HideEmergencySleepPanel()
    {
        emergencyPanelSleep.SetActive(false);

        StopCoroutine(StartTimer(timerText.text));
    }

    private IEnumerator StartTimer(string timerText)
    {
        for (int i = 15; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            timerText = i.ToString();
        }
    }
}