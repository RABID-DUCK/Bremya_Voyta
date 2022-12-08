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
        Debug.Log("dasd");
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
