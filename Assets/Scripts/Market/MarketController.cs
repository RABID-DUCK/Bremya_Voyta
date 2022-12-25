using Photon.Pun;
using System;

public class MarketController : MonoBehaviourPunCallbacks
{
    public event Action OnOpenMarket;
    public event Action OnCloseMarket;

    public Character character { get; private set; }

    [Serializable]
    public struct MarketPrices
    {
        public int woodPrice;
        public int berriesPrice;
        public int carrotPrice;
        public int milkPrice;
        public int coalPrice;
        public int ironPrice;
        public int meatPrice;
        public int fishPrice;
    }

    public MarketPrices marketPrices;

    private void Start()
    {
        character = photonView.gameObject.GetComponent<Character>();

        if (!character)
        {
            return;
        }
    }

    public void OpenMarket()
    {
        OnOpenMarket?.Invoke();
    }

    public void CloseMareket()
    {
        OnCloseMarket?.Invoke();
    }
}
