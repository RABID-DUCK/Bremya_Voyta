using System;
using UnityEngine;
using UnityEngine.UI;

public class CasinoView : MonoBehaviour
{
    [SerializeField] private Button minimumRateButton;
    [SerializeField] private Button averageRateButton;
    [SerializeField] private Button maximumRateButton;

    public event Action OnClickMinimumRateButton = delegate { };
    public event Action OnClickAverageRateButton = delegate { };
    public event Action OnClickMaximumRateButton = delegate { };

    void Start()
    {
        minimumRateButton.onClick.AddListener(SendOnClickMinimumRateButton);
        averageRateButton.onClick.AddListener(SendOnClickAverageRateButton);
        maximumRateButton.onClick.AddListener(SendOnClickMaximumRateButton);
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
}