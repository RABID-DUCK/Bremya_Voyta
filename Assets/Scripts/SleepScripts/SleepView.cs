using System;
using UnityEngine;
using UnityEngine.UI;

public class SleepView : MonoBehaviour
{
    [Header("UI panel")]
    [Tooltip("The panel that comes out when you click on the bed")]
    [SerializeField] private GameObject sleepSelectionPanel;

    [Header("UI buttons")]
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [SerializeField] private Outline bedOutline;

    public event Action OnClickYesButton;
    public event Action OnClickNoButton;

    private void Start()
    {
        yesButton.onClick.AddListener(SendPositiveResponse);
        noButton.onClick.AddListener(SendNegativeResponse);
    }

    private void OnMouseOver()
    {
        bedOutline.enabled = true;

        if (Input.GetMouseButtonDown(0))
        {
            ShowSleepPanel(sleepSelectionPanel);
        }
    }

    private void SendPositiveResponse()
    {
        OnClickYesButton?.Invoke();
    }

    private void SendNegativeResponse()
    {
        OnClickNoButton?.Invoke();

        HideSleepPanel(sleepSelectionPanel);
    }

    private void ShowSleepPanel(GameObject sleepSelectionPanel)
    {
        sleepSelectionPanel.SetActive(true);
    }

    private void HideSleepPanel(GameObject sleepSelectionPanel)
    {
        sleepSelectionPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        yesButton.onClick.RemoveListener(SendPositiveResponse);
        noButton.onClick.RemoveListener(SendNegativeResponse);
    }
}