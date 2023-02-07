using System;
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

    public List<Image> imageResuces = new List<Image>();
    public List<TMP_Text> nameResurcesText = new List<TMP_Text>();
    public List<TMP_Text> countResurcesText = new List<TMP_Text>();

    public GameObject standartText;

    private ShowCanvasGroup showCanvasGroup;

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

    public void ShowTaxHasNotBeenPaidPanel()
    {
        UIController.ShowInfo("У вас не хватает ресурсов для уплаты налога!", "Ок");
    }
}