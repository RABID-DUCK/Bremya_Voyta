using UnityEngine;

public class Coordinator : MonoBehaviour
{
    [Header("Market")]
    [SerializeField] private MarketController marketController;
    [SerializeField] ShowCanvasGroup marketShowCanvasGroup;

    private void Start()
    {
        marketController.OnOpenMarket += OpenMarketPanel;
        marketController.OnCloseMarket += CloseMarketPanel;

        if(marketShowCanvasGroup != null)
        {
            marketShowCanvasGroup.FastHide();
        }
    }

    private void OpenMarketPanel()
    {
        if (marketShowCanvasGroup != null)
        {
            marketShowCanvasGroup.Show();
        }
    }

    private void CloseMarketPanel()
    {
        if (marketShowCanvasGroup != null)
        {
            marketShowCanvasGroup.Hide();
        }
    }
}
