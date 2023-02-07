using System;
using System.Collections.Generic;
using UnityEngine;

public class TaxBoxPresenter : TaxBoxModel
{
    [SerializeField] private TaxBox taxBoxView;
    [SerializeField] private TaxBoxPanelView taxBoxPanelView;
    [SerializeField] private ShowCanvasGroup showCanvasGroup;

    [SerializeField] private List<Item> resurces = new List<Item>();

    private WorldTime worldTime;

    private Character player;

    private void Start()
    {
        worldTime.OnStartTaxEvent += StartTaxEvent;

        taxBoxView.OnClickTaxBox += OpenTaxBoxPanel;

        taxBoxPanelView.OnClickGetResource += TakeResourcesFromPlayer;
    }

    public void Initialization(Character player)
    {
        this.player = player;
    }

    private void StartTaxEvent()
    {
        IsPanelCanBeOpened();

        SetInformationAboutNecessaryResources();
    }

    private void IsPanelCanBeOpened()
    {
        worldTime.OnStartTaxEvent -= StartTaxEvent;

        worldTime.OnStopTaxEvent += OutputtingTaxBoxEventResults;

        taxBoxPanelView.ShowTrue();
    }

    private void OpenTaxBoxPanel()
    {
        taxBoxPanelView.ShowTaxBoxPanel();
    }

    private void SetInformationAboutNecessaryResources()
    {
        //taxBoxPanelView.ShowAllElements();

        SelectRandomResurses(resurces);

        SetSelectedResurcesInformationOnTaxBoxPanel(taxBoxPanelView.imageResuces, taxBoxPanelView.nameResurcesText, taxBoxPanelView.countResurcesText);
    }

    private void OutputtingTaxBoxEventResults()
    {
        if (taxBoxPanelView.isCompleted == false)
        {
            taxBoxPanelView.ShowTaxHasNotBeenPaidPanel();

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
            taxBoxPanelView.ShowTaxHasNotBeenPaidPanel();
        }
    }

    public void OpenPanelDuringStartEvent()
    {
        showCanvasGroup.Show();
    }
}
