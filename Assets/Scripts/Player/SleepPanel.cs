using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepPanel : MonoBehaviour
{
    //private bool yesBtn;
    //private bool noBtn;
    //
    //[SerializeField] private Button warningYes;
    //
    //[SerializeField] private GameObject player;
    //[SerializeField] private GameObject sleepWindow;
    //[SerializeField] private GameObject warningWindow;

    //float notSleep=0;
    //
    //private void Start()
    //{
    //    
    //}
    //public void BtnYes()
    //{
    //    yesBtn = true;
    //    ShowSleepPanel();
    //}
    //public void BtnNo()
    //{
    //    noBtn = true;
    //}
    //public void ShowSleepPanel()
    //{
    //    if (yesBtn == true)
    //    {
    //        sleepWindow.SetActive(false);
    //        Instantiate(player, new Vector3(-0.984f, 0.4f, -0.261f), Quaternion.LookRotation(new Vector3(0,90f,0)));
    //    }
    //    else if (noBtn == true)
    //    {
    //        if (notSleep == 0)
    //        {
    //            sleepWindow.SetActive(false);
    //            notSleep += 1;
    //        }
    //        else if (notSleep >= 1)
    //        {
    //            sleepWindow.SetActive(false);
    //            //warningWindow.SetActive(true);
    //            Instantiate(player, player.transform.position, Quaternion.LookRotation(new Vector3(0,90f,0)));
    //        }
    //    }
    //}

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject sleepPanel;

    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    public event Action OnSleep;

    private void Start()
    {
        yesButton.onClick.AddListener(SleepSender);
        noButton.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        sleepPanel.SetActive(false);
    }

    private void SleepSender()
    {
        OnSleep?.Invoke();
    }
}
