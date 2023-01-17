using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningSleepPanel : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;

    [SerializeField] private GameObject warningSleepPanel;

    private bool IsSleep;

    [SerializeField] private float timeStart;
    [SerializeField] private TextMeshProUGUI textTimer;

    public event Action<bool> OnDontSleep = delegate { };

    private void Start()
    {
        textTimer.text = timeStart.ToString();

        worldTime.GetTimeOfDay += ShowWarningSleepPanel;
    }

    public void ShowWarningSleepPanel(bool timeOfDay)
    {
        warningSleepPanel.SetActive(true);

        timeStart -= Time.deltaTime;
        textTimer.text = ("Пора спать! У вас " + (Mathf.Round(timeStart).ToString()) + " секунд чтобы лечь спать!");
    }

    public void HideWarningSleepPanel()
    {
        StartCoroutine(HidePanel());

        IsSleep = false;

        OnDontSleep?.Invoke(IsSleep);
    }

    public IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(15f);
        warningSleepPanel.SetActive(false);
    }
}
