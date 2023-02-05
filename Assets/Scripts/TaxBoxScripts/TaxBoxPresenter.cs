using System;
using System.Collections.Generic;
using UnityEngine;

public class TaxBoxPresenter : TaxBoxModel
{
    [SerializeField] private TaxBox taxBoxView;
    [SerializeField] private TaxBoxPanelsView taxBoxPanelsView;

    [SerializeField] private List<Item> resurces = new List<Item>();

    private WorldTime worldTime;

    private Character player;

    private void Awake()
    {
        worldTime = FindObjectOfType<WorldTime>();
    }

    private void Start()
    {
        taxBoxView.OnClickTaxBox += OpenTaxBoxPanel;

        worldTime.OnStartTaxEvent += StartTaxEvent;

        worldTime.OnStopTaxEvent += OutputtingTaxBoxEventResults;

        taxBoxPanelsView.OnClickGetResource += TakeResourcesFromPlayer;
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
        taxBoxPanelsView.ShowTrue();
    }

    private void OpenTaxBoxPanel()
    {
        taxBoxPanelsView.ShowTaxBoxPanel();
    }

    private void SetInformationAboutNecessaryResources()
    {
        SelectRandomResurses(resurces);

        SetSelectedResurcesInformationOnTaxBoxPanel(taxBoxPanelsView.imageResuces, taxBoxPanelsView.nameResurcesText, taxBoxPanelsView.countResurcesText);
    }

    private void OutputtingTaxBoxEventResults()
    {
        if (taxBoxPanelsView.isCompleted == false)
        {
            taxBoxPanelsView.ShowTaxHasNotBeenPaidPanel();
        }
    }

    private void TakeResourcesFromPlayer()
    {
        try
        {
            GetResourcesFromPlayer(player);

            taxBoxPanelsView.ShowSuccessfullyPanel();
        }
        catch (InvalidOperationException)
        {
            taxBoxPanelsView.ShowTaxHasNotBeenPaidPanel();
        }
    }
}
