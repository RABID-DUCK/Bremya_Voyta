using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningSleepPanel : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;
    [SerializeField] private SleepPanel sleepPanel;

    [SerializeField] private GameObject warningSleepObject;
    [SerializeField] private GameObject warningSleepPanel;

    private bool IsSleep;

    [SerializeField] private float timeStart;
    [SerializeField] private TextMeshProUGUI textTimer;

    public event Action<bool> OnDontSleep = delegate { };
    private void Awake()
    {
        DontDestroyOnLoad(warningSleepObject);
    }
    private void Start()
    {
        textTimer.text = timeStart.ToString();

        worldTime.GetTimeOfDay += ShowWarningSleepPanel;
    }

    private void Update()
    {
        ShowWarningSleepPanel(worldTime.CheckTimeOfDay);
    }

    public void ShowWarningSleepPanel(bool timeOfDay)
    {
        if (!timeOfDay)
        {
            warningSleepPanel.SetActive(true);

            if(timeStart > 0)
            {
                timeStart -= Time.deltaTime;

                textTimer.text = ("Пора спать! У вас " + (Mathf.Round(timeStart).ToString()) + " секунд чтобы лечь спать!");
            }
            else
            {
                HideWarningSleepPanel();
            }
        }
    }

    public void HideWarningSleepPanel()
    {
        StartCoroutine(HidePanel());
    }

    public IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(15f);
        warningSleepPanel.SetActive(false);
    }
}
