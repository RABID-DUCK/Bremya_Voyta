using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class TaxBoxPresenter : TaxBoxModel, IObjectWithCharacter
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

    public void InitializePlayer(Character player)
    {
        this.player = player;
    }

    private void StartTaxEvent()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "StartTax", "Start" } });
        }
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
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Tax", "End" } });
        }
    }

    private void TakeResourcesFromPlayer()
    {
        bool isCheckResource = GetResourcesFromPlayer(player);

        if (isCheckResource)
        {
            taxBoxPanelView.ShowSuccessfullyPanel();

            taxBoxPanelView.HideTaxBoxPanel();
        }
        else
        {
            UIController.ShowInfo("У вас не хватает ресурсов для уплаты налога!", "Ок");
        }
    }

    private string tax = "";
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("Tax"))
        {
            tax = (string)propertiesThatChanged["Tax"];
            if (tax == "Start")
            {
                IsPanelCanBeOpened();

                SetInformationAboutNecessaryResources();

                worldTimeEventSender.OnStartTaxEvent -= StartTaxEvent;
                worldTimeEventSender.OnStopTaxEvent += OutputtingTaxBoxEventResults;
            }
            else 
            if(tax == "End")
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
        }
    }
}