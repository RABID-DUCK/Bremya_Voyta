using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] GameObject marketPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMarketPanel()
    {
        if (marketPanel != null)
        {
            bool IsActive = marketPanel.activeSelf;
            marketPanel.SetActive(IsActive);
        }
    }

    public void CloseMarketPanel()
    {
        marketPanel.SetActive(false);
    }

}
