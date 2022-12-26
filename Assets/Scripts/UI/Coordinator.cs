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
        marketShowCanvasGroup.FastHide();
    }

    private void OpenMarketPanel()
    {
        marketShowCanvasGroup.Show();
    }

    private void CloseMarketPanel()
    {
        marketShowCanvasGroup.Hide();
    }
}
