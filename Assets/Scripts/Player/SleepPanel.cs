using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SleepPanel : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;

    private Character player;

    [SerializeField] private List<House> houses;

    [SerializeField] private GameObject sleepPanel;

    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [SerializeField] private VideoClip sleepVideo;

    private CharacterController _charackter;

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
        
    }
}
