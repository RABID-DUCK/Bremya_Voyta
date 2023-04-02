using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class CasinoPresenter : MonoBehaviourPunCallbacks
{
    [SerializeField] private CasinoView casinoView;
    [SerializeField] private ShopController marketController;

    [SerializeField] private WorldTimeEventSender worldTimeEventSender;

    private float countCoinsSelectedEvent;
    private int amountOfMoneyWon;

    private bool isUseSystem;

    private void Start()
    {
        worldTimeEventSender.OnStartCasinoEvent += ShowCasinoHelloPanel;

        casinoView.OnClickMinimumRateButton += GetMinimumRateMoney;
        casinoView.OnClickAverageRateButton += GetAvarageRateMoney;
        casinoView.OnClickMaximumRateButton += GetMaximumRateMoney;
    }

    private void ShowCasinoHelloPanel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "StartCasino", "" } });
        }
    }

    private void GetMinimumRateMoney()
    {
        marketController.GetMoney(5);

        countCoinsSelectedEvent = 5;

        isUseSystem = true;

        casinoView.CloseCasino();
    }

    private void GetAvarageRateMoney()
    {
        marketController.GetMoney(10);

        countCoinsSelectedEvent = 10;

        isUseSystem = true;

        casinoView.CloseCasino();
    }

    private void GetMaximumRateMoney()
    {
        marketController.GetMoney(15);

        countCoinsSelectedEvent = 15;

        isUseSystem = true;

        casinoView.CloseCasino();
    }

    private void CheckingProbabilityWinningMoney()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            casinoView.CloseCasino();

            if (isUseSystem)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "EndCasino", marketController.CalculateProbabilityWinning() ? 1 : 0 } });
            }
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("StartCasino"))
        {
            UIController.ShowInfo($"Началось событие менялы!\r\nИспытайте свою удачу!", "Ок");

            casinoView.OpenCasino();

            worldTimeEventSender.OnStartCasinoEvent -= ShowCasinoHelloPanel;
            worldTimeEventSender.OnStopCasinoEvent += CheckingProbabilityWinningMoney;
        }

        if (propertiesThatChanged.ContainsKey("EndCasino"))
        {
            if ((int)propertiesThatChanged["EndCasino"] == 0) // если проиграл
            {
                UIController.ShowInfo("Вы програли свою ставку!", "Ок");

                isUseSystem = false;

                casinoView.CloseCasino();
            }
            else
            {
                marketController.CalculateWinningAmount(countCoinsSelectedEvent, out amountOfMoneyWon);

                marketController.SetWinMoney(amountOfMoneyWon);

                UIController.ShowInfo($"Ваша ставка выиграла! Вы получаете дополнительно {amountOfMoneyWon} монет!", "Ок");

                countCoinsSelectedEvent = 0;

                isUseSystem = false;

                casinoView.CloseCasino();
            }

            worldTimeEventSender.OnStartCasinoEvent += ShowCasinoHelloPanel;
            worldTimeEventSender.OnStopCasinoEvent -= CheckingProbabilityWinningMoney;
        }
    }

    private void OnDestroy()
    {
        worldTimeEventSender.OnStartCasinoEvent -= ShowCasinoHelloPanel;

        casinoView.OnClickMinimumRateButton -= GetMinimumRateMoney;
        casinoView.OnClickAverageRateButton -= GetAvarageRateMoney;
        casinoView.OnClickMaximumRateButton -= GetMaximumRateMoney;
    }
}
