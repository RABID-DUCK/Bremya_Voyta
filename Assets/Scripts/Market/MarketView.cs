using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MarketView : MonoBehaviour
{
    [SerializeField] private Button btnMarketPlace;
    [SerializeField] private Button btnChangerPlace;
    [SerializeField] private Button btnStorePlace;

    public List<GameObject> marketPanels = new List<GameObject>();

    public int panelId = 0;
    public event Action<int> selectPanelId;

    private void Start()
    {
        btnMarketPlace.onClick.AddListener(SelectMarketPlacePanel);
        btnChangerPlace.onClick.AddListener(SelectChangerPlacePanel);
        btnStorePlace.onClick.AddListener(SelectStorePlacePanel);

        selectPanelId?.Invoke(panelId);
    }
    public void SelectMarketPlacePanel()
    {
        panelId = 2;
    }

    public void SelectChangerPlacePanel()
    {
        panelId = 0;
    }

    public void SelectStorePlacePanel()
    {
        panelId = 1;
    }

}
