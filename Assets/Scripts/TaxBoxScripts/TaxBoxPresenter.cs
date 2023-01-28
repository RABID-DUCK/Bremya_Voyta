using System.Collections.Generic;
using UnityEngine;

public class TaxBoxPresenter : TaxBoxModel
{
    [SerializeField] private TaxBox taxBoxView;
    [SerializeField] private TaxBoxPanelsView taxBoxPanelsView;
 
    [SerializeField] private List<Item> resurces = new List<Item>();

    private WorldTime worldTime;

    private void Awake()
    {
        worldTime = FindObjectOfType<WorldTime>();
    }

    private void Start()
    {
        taxBoxView.OnClickTaxBox += OpenTaxBoxPanel;

        worldTime.OnStartTaxEvent += SetInformationAboutNecessaryResources;

        taxBoxPanelsView.OnClickGetResource += TakeResourcesFromPlayer;
    }

    private void OpenTaxBoxPanel()
    {
        if(taxBoxPanelsView.isCompleted == false)
            taxBoxPanelsView.ShowTaxBoxPanel();
    }

    private void SetInformationAboutNecessaryResources()
    {
        SelectRandomResurses(resurces);

        SetSelectedResurcesInformationOnTaxBoxPanel(taxBoxPanelsView.imageResuces, taxBoxPanelsView.nameResurcesText, taxBoxPanelsView.countResurcesText);
    }

    private void TakeResourcesFromPlayer()
    {
        if(CheckingResources() == true)
        {
            //TODO: Дописать

            taxBoxPanelsView.ShowSuccessfullyPanel();
        }
        else
        {
            taxBoxPanelsView.ShowErrorPanel();
        }
    }
}
