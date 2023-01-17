using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepPanel : MonoBehaviour
{
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
