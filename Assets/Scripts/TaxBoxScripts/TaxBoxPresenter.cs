using System;
using System.Collections.Generic;
using UnityEngine;

public class TaxBoxPresenter : TaxBoxModel
{
    [SerializeField] private WorldTime worldTime;

    [Space ,SerializeField] private TaxBox taxBox;
    [SerializeField] private TaxBoxPanelView taxBoxPanelView;
    [SerializeField] private ShowCanvasGroup showCanvasGroup;

    [SerializeField] private List<Item> resurces = new List<Item>();

    private Character player;

    private void Start()
    {
        worldTime.OnStartTaxEvent += StartTaxEvent;

        taxBox.OnClickTaxBox += OpenTaxBoxPanel;

        taxBoxPanelView.OnClickGetResource += TakeResourcesFromPlayer;
    }

    public void PlayerInitialization(Character player)
    {
        this.player = player;
    }

    private void StartTaxEvent()
    {
        worldTime.OnStartTaxEvent -= StartTaxEvent;

        IsPanelCanBeOpened();

        SetInformationAboutNecessaryResources();
    }

    private void IsPanelCanBeOpened()
    {
        worldTime.OnStopTaxEvent += OutputtingTaxBoxEventResults;

        taxBoxPanelView.ShowTrue();
    }

    private void OpenTaxBoxPanel()
    {
        taxBoxPanelView.ShowTaxBoxPanel();
    }

    private void SetInformationAboutNecessaryResources()
    {
        SelectRandomResurses(resurces);

        SetSelectedResurcesInformationOnTaxBoxPanel(taxBoxPanelView.imageResuces, taxBoxPanelView.nameResurcesText, taxBoxPanelView.countResurcesText);
    }

    private void OutputtingTaxBoxEventResults()
    {
        if (taxBoxPanelView.isCompleted == false)
        {
            taxBoxPanelView.ShowPanelTaxNotPaid();

            TakePenaltyForNonPaymentOfTax(player);

            worldTime.OnStopTaxEvent -= OutputtingTaxBoxEventResults;
        }
    }

    private void TakeResourcesFromPlayer()
    {
        try
        {
            GetResourcesFromPlayer(player);

            taxBoxPanelView.ShowSuccessfullyPanel();
        }
        catch (InvalidOperationException)
        {
            taxBoxPanelView.ShowPanelNotEnoughResources();
        }
    }

    public void OpenPanelDuringStartEvent()
    {
        showCanvasGroup.Show();
    }
}
