using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketPresenter : MonoBehaviour
{
    [SerializeField] private GameObject marketPanel;
    [SerializeField] public MarketView marketView;

    private int currentId;


    private void Start()
    {
        marketView.selectPanelId += SetPanelId;
    }

    private void SetPanelId(int panelId)
    {
        currentId = panelId;
        for (int i = 0; i < marketView.marketPanels.Count; i++)
        {
            if (currentId == i) 
            {
                marketView.marketPanels[i].SetActive(true);
            } 
            else {
                marketView.marketPanels[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
    }

    public void OpenMarketPanel()
    {
        marketPanel.SetActive(true);
    }

    public void CloseMarketPanel()
    {
        marketPanel.SetActive(false);
    }
}
