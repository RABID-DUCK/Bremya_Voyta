﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxBoxPanelView : MonoBehaviour
{
    [Header("TaxBox Panel")]
    [SerializeField] private GameObject taxBoxPanel;
    [SerializeField] private Button getResourceButton;
    [SerializeField] private Button closeTaxBoxPanelButton;

    [Header("UI Elements")]
    public List<TaxBoxViewItem> TaxBoxItems = new List<TaxBoxViewItem>();

    [Space]
    [SerializeField] private ShowCanvasGroup showCanvasGroup;

    [Space]
    public bool isPanelCanBeOpened = false;

    public bool isCompleted { get; private set; } = false;

    public event Action OnClickGetResource;

    private void Start()
    {
        getResourceButton.onClick.AddListener(SendOnClickGetResource);
        closeTaxBoxPanelButton.onClick.AddListener(HideTaxBoxPanel);
    }

    public void ShowTrue()
    {
        isPanelCanBeOpened = true;

        UIController.ShowInfo("Оплатите налоги!!!", "Ок");
    }

    private void SendOnClickGetResource()
    {
        OnClickGetResource?.Invoke();
    }

    public void ShowTaxBoxPanel()
    {
        if (isPanelCanBeOpened)
        {
            showCanvasGroup.Show();
        }
        else
        {
            UIController.ShowInfo("Событие пока не доступно!", "Ок"); 
        }
    }

    public void HideTaxBoxPanel()
    {
        showCanvasGroup.Hide();
    }

    public void ShowSuccessfullyPanel()
    {
        UIController.ShowInfo("Налог успешно уплачен!", "Ок", IsCompleted);
    }

    private void IsCompleted()
    {
        isCompleted = true;
    }

    public void ShowPanelNotEnoughResources()
    {
        UIController.ShowInfo("У вас не хватает ресурсов для уплаты налога!", "Ок");
    }

    public void ShowPanelTaxNotPaid()
    {
        UIController.ShowInfo("Налог не улачен! Штраф составляет 10 монет!", "Ок");
    }
}

[Serializable]
public class TaxBoxViewItem
{
    public Image image;

    public TMP_Text nameText;

    public TMP_Text countText;
}