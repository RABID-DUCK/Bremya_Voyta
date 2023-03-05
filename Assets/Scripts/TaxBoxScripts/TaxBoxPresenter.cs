using System.Collections.Generic;
using UnityEngine;

public class TaxBoxPresenter : TaxBoxModel
{
    [SerializeField] private WorldTimeEventSender worldTimeEventSender;

    [Header("View scripts")]
    [SerializeField] private TaxBox taxBox;
    [SerializeField] private TaxBoxPanelView taxBoxPanelView;

    [SerializeField] private List<Item> resurces = new List<Item>();

    [Space]
    [SerializeField] private ShowCanvasGroup showCanvasGroup;


    private Character player;

    private void Start()
    {
        worldTimeEventSender.OnStartTaxEvent += StartTaxEvent;

        taxBox.OnClickTaxBox += OpenTaxBoxPanel;

        taxBoxPanelView.OnClickGetResource += TakeResourcesFromPlayer;
    }

    public void PlayerInitialization(Character player)
    {
        this.player = player;
    }

    private void StartTaxEvent()
    {
        IsPanelCanBeOpened();

        SetInformationAboutNecessaryResources();

        worldTimeEventSender.OnStartTaxEvent -= StartTaxEvent;
        worldTimeEventSender.OnStopTaxEvent += OutputtingTaxBoxEventResults;
    }

    private void IsPanelCanBeOpened()
    {
        taxBoxPanelView.ShowTrue();
    }

    private void OpenTaxBoxPanel()
    {
        taxBoxPanelView.ShowTaxBoxPanel();
    }

    private void SetInformationAboutNecessaryResources()
    {
        SelectRandomResurses(resurces);

        SetSelectedResurcesInformationOnTaxBoxPanel(taxBoxPanelView.TaxBoxItems);
    }

    private void OutputtingTaxBoxEventResults()
    {
        if (taxBoxPanelView.isCompleted == false)
        {
            taxBoxPanelView.ShowPanelTaxNotPaid();

            TakePenaltyForNonPaymentOfTax(player);

            taxBoxPanelView.HideTaxBoxPanel();

            taxBoxPanelView.isPanelCanBeOpened = false;

            worldTimeEventSender.OnStopTaxEvent -= OutputtingTaxBoxEventResults;
        }
    }

    private void TakeResourcesFromPlayer()
    {
        if (GetResourcesFromPlayer(player))
        {
            taxBoxPanelView.ShowSuccessfullyPanel();
        }
        else
        {
            UIController.ShowInfo("У вас не хватает ресурсов для уплаты налога!", "Ок");
        }
    }
}