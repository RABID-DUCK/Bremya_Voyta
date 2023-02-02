﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxBoxPanelsView : MonoBehaviour
{
    [Header("TaxBox Panel")]
    [SerializeField] private GameObject taxBoxPanel;
    [SerializeField] private Button getResourceButton;
    [SerializeField] private Button closeTaxBoxPanelButton;

    public List<Image> imageResuces = new List<Image>();
    public List<TMP_Text> nameResurcesText = new List<TMP_Text>();
    public List<TMP_Text> countResurcesText = new List<TMP_Text>();

    [Header("Successfully Panel")]
    [SerializeField] private GameObject successfullyPanel;
    [SerializeField] private Button okSuccessfullyButton;

    [Header("Error Panel")]
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private Button okErrorButton;

    [Header("Has Not Been Paid Panel")]
    [SerializeField] private GameObject taxHasNotBeenPaidPanel;
    [SerializeField] private Button okButton;

    public event Action OnClickGetResource;

    public bool isPanelCanBeOpened = false;

    public bool isCompleted { get; private set; } = false;

    private void Start()
    {
        getResourceButton.onClick.AddListener(SendOnClickGetResource);
        closeTaxBoxPanelButton.onClick.AddListener(HideTaxBoxPanel);

        okSuccessfullyButton.onClick.AddListener(IsCompleted);

        okErrorButton.onClick.AddListener(HideErrorPanel);

        okButton.onClick.AddListener(HideTaxHasNotBeenPaidPanel);
    }

    public void ShowTrue()
    {
        isPanelCanBeOpened = true;
    }

    private void SendOnClickGetResource()
    {
        OnClickGetResource?.Invoke();
    }

    public void ShowTaxBoxPanel()
    {
        if(isPanelCanBeOpened)
            taxBoxPanel.SetActive(true);
    }

    public void HideTaxBoxPanel()
    {
        taxBoxPanel.SetActive(false);
    }

    public void ShowSuccessfullyPanel()
    {
        successfullyPanel.SetActive(true);
    }

    private void HideSuccessfullyPanel()
    {
        successfullyPanel.SetActive(false);
    }

    private void IsCompleted()
    {
        isCompleted = true;
        HideSuccessfullyPanel();
    }

    public void ShowErrorPanel()
    {
        errorPanel.SetActive(true);
    }

    private void HideErrorPanel()
    {
        errorPanel.SetActive(false);
    }

    public void ShowTaxHasNotBeenPaidPanel()
    {
        taxHasNotBeenPaidPanel.SetActive(true);
    }

    private void HideTaxHasNotBeenPaidPanel()
    {
        taxHasNotBeenPaidPanel.SetActive(false);
    }
}