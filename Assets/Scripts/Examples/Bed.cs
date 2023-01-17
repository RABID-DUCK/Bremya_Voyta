using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Bed : MonoBehaviour
{
    Outline outline;

    [SerializeField] private SleepPanel sleepPanel;
    [SerializeField] private LayerMask sleepLayerMask;
    [SerializeField] private GameObject sleepWindow;
    [SerializeField] private GameObject player;

    [SerializeField] private WarningSleepPanel warningSleepPanel;
    private bool IsSleep { get; set; }

    private void Start()
    {
        outline = transform.gameObject.GetComponent<Outline>();

        sleepPanel.OnSleep += Sleep;

        //warningSleepPanel.OnDontSleep += ForcedSleep;
    }

    private void OnMouseOver()
    {
        outline.enabled = true;

        if (Input.GetMouseButtonDown(0))
        {
            StateSleep();
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    private void Update()
    {
        if (WorldTime.CheckTimeOfDay == true)
        {
            Instantiate(player, new Vector3(0.2f, 0.08f, 0.7f), Quaternion.LookRotation(new Vector3(0, 0, 0)));

            IsSleep = false;
        }
        if (WorldTime.CheckTimeOfDay == false)
        {
            warningSleepPanel.ShowWarningSleepPanel(WorldTime.CheckTimeOfDay);
        }
    }

    private void StateSleep()
    {
        if (WorldTime.CheckTimeOfDay == false)
        {
            print("night");
            sleepWindow.SetActive(true);
        }
        else
        {
            print("day");
            print(WorldTime.CheckTimeOfDay);
        }
    }

    private void Sleep()
    {
        Instantiate(player, new Vector3(-0.984f, 0.4f, -0.261f), Quaternion.LookRotation(new Vector3(0, 90f, 0)));

        IsSleep = true;
    }
}

