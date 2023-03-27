using System;
using UnityEngine;
using UnityEngine.UI;

public class CasinoView : MonoBehaviour
{
    public event Action OnClickMinimumRateButton;
    public event Action OnClickAverageRateButton;
    public event Action OnClickMaximumRateButton;

    [SerializeField] private Button minimumRateButton;
    [SerializeField] private Button averageRateButton;
    [SerializeField] private Button maximumRateButton;

    private void Start()
    {
        minimumRateButton.onClick.AddListener(SendOnClickMinimumRateButton);
        averageRateButton.onClick.AddListener(SendOnClickAverageRateButton);
        maximumRateButton.onClick.AddListener(SendOnClickMaximumRateButton);

        CloseCasino();
    }

    private void SendOnClickMinimumRateButton()
    {
        OnClickMinimumRateButton?.Invoke();
    }

    private void SendOnClickAverageRateButton()
    {
        OnClickAverageRateButton?.Invoke();
    }

    private void SendOnClickMaximumRateButton()
    {
        OnClickMaximumRateButton?.Invoke();
    }

    public void OpenCasino()
    {
        minimumRateButton.interactable = true;
        averageRateButton.interactable = true;
        maximumRateButton.interactable = true;
    }

    public void CloseCasino()
    {
        minimumRateButton.interactable = false;
        averageRateButton.interactable = false;
        maximumRateButton.interactable = false;
    }

    private void OnDestroy()
    {
        minimumRateButton.onClick.RemoveListener(SendOnClickMinimumRateButton);
        averageRateButton.onClick.RemoveListener(SendOnClickAverageRateButton);
        maximumRateButton.onClick.RemoveListener(SendOnClickMaximumRateButton);
    }
}