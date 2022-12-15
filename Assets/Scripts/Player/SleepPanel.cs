using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepPanel : MonoBehaviour
{
    private bool yesBtn;
    private bool noBtn;

    PlayerController playerController;

    [SerializeField] private Button warningYes;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSleep;
    [SerializeField] private GameObject sleepWindow;
    [SerializeField] private GameObject warningWindow;

    float notSleep=0;
    public void BtnYes()
    {
        yesBtn = true;
        ShowSleepPanel();
    }
    public void BtnNo()
    {
        noBtn = true;
        ShowSleepPanel();
    }
    public void ShowSleepPanel()
    {
        if (yesBtn==true)
        {
            sleepWindow.SetActive(false);
            playerSleep.SetActive(true);
            player.SetActive(false);
        }
        else if (noBtn == true)
        {
            if (notSleep == 0)
            {
                sleepWindow.SetActive(false);
                notSleep += 1;
            }
            else if (notSleep >= 1)
            {
                sleepWindow.SetActive(false);
                warningWindow.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSleep.SetActive(false);
            player.SetActive(true);
        }
    }
    public void WarningBtnYes()
    {
        warningWindow.SetActive(false);
        playerSleep.SetActive(true);
        player.SetActive(false);
    }
    public void WarningBtnNo()
    {
        print("тут он будет валиться на камнях");
    }
}
