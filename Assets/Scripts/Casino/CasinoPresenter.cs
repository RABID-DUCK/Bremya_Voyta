using UnityEngine;

public class CasinoPresenter : MonoBehaviour
{
    [SerializeField] private CasinoView casinoView;
    [SerializeField] private ShopController marketController;

    [SerializeField] private WorldTimeEventSender worldTimeEventSender;

    private int countCoinsSelectedEvent;
    private int amountOfMoneyWon;

    private void Start()
    {
        worldTimeEventSender.OnStartCasinoEvent += ShowCasinoHelloPanel;

        casinoView.OnClickMinimumRateButton += GetMinimumRateMoney;
        casinoView.OnClickAverageRateButton += GetAvarageRateMoney;
        casinoView.OnClickMaximumRateButton += GetMaximumRateMoney;
    }

    private void ShowCasinoHelloPanel()
    {
        UIController.ShowInfo($"Началось событие менялы!\r\nИспытайте свою удачу!", "Ок");

        casinoView.OpenCasino();

        worldTimeEventSender.OnStartCasinoEvent -= ShowCasinoHelloPanel;
        worldTimeEventSender.OnStopCasinoEvent += CheckingProbabilityWinningMoney;
    }

    private void GetMinimumRateMoney()
    {
        marketController.GetMoney(5);

        countCoinsSelectedEvent = 5;

        casinoView.CloseCasino();
    }

    private void GetAvarageRateMoney()
    {
        marketController.GetMoney(10);

        countCoinsSelectedEvent = 10;

        casinoView.CloseCasino();
    }

    private void GetMaximumRateMoney()
    {
        marketController.GetMoney(15);

        countCoinsSelectedEvent = 15;

        casinoView.CloseCasino();
    }

    private void CheckingProbabilityWinningMoney()
    {
        bool isWon = marketController.CalculateProbabilityWinning();

        if (isWon == false)
        {
            UIController.ShowInfo("Вы програли свою ставку!", "Ок");

            casinoView.CloseCasino();
        }
        else
        {
            marketController.CalculateWinningAmount(countCoinsSelectedEvent, out amountOfMoneyWon);

            marketController.SetWinMoney(amountOfMoneyWon);

            UIController.ShowInfo($"Ваша ставка выиграла! Вы получаете дополнительно {amountOfMoneyWon} монет!", "Ок");
        }

        countCoinsSelectedEvent = 0;

        worldTimeEventSender.OnStartCasinoEvent += ShowCasinoHelloPanel;
        worldTimeEventSender.OnStopCasinoEvent -= CheckingProbabilityWinningMoney;
    }
}
