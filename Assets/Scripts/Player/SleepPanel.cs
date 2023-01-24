using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SleepPanel : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject sleepPanel;

    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [SerializeField] private VideoClip sleepVideo;

    public event Action OnSleep;

    private void Start()
    {
        yesButton.onClick.AddListener(Sleeping);
        noButton.onClick.AddListener(CloseSleepPanel);
    }

    private void CloseSleepPanel()
    {
        sleepPanel.SetActive(false);
    }

    public void Sleeping()
    {
        OnSleep?.Invoke();

        sleepPanel.SetActive(false);

        UIController.ShowVideo(sleepVideo, AfterSleep);
    }

    private void AfterSleep()
    {
        SceneManager.LoadScene("CityScene");
    }
}
