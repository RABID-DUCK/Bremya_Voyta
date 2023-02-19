using System.Collections.Generic;
using UnityEngine;

public class TaxBoxPresenter : TaxBoxModel
{
    [SerializeField] private WorldTimeEventSender worldTimeEventSender;

    [Space, SerializeField] private TaxBox taxBox;
    [SerializeField] private TaxBoxPanelView taxBoxPanelView;
    [SerializeField] private ShowCanvasGroup showCanvasGroup;

    [SerializeField] private List<Item> resurces = new List<Item>();

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
        worldTimeEventSender.OnStartTaxEvent -= StartTaxEvent;
        worldTimeEventSender.OnStopTaxEvent += OutputtingTaxBoxEventResults;

        IsPanelCanBeOpened();

        SetInformationAboutNecessaryResources();
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

        SetSelectedResurcesInformationOnTaxBoxPanel(taxBoxPanelView.imageResuces, taxBoxPanelView.nameResurcesText, taxBoxPanelView.countResurcesText);
    }

    private void OutputtingTaxBoxEventResults()
    {
        if (taxBoxPanelView.isCompleted == false)
        {
            taxBoxPanelView.ShowPanelTaxNotPaid();

            TakePenaltyForNonPaymentOfTax(player);

            taxBoxPanelView.HideTaxBoxPanel();

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

    public void OpenPanelDuringStartEvent()
    {
        showCanvasGroup.Show();
    }
}
