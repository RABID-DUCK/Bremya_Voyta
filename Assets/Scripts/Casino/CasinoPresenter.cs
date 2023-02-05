using UnityEngine;

public class CasinoPresenter : MonoBehaviour
{
    [SerializeField] private CasinoView casinoView;
    [SerializeField] private MarketController marketController;

    [SerializeField] private WorldTime worldTime;

    private int countCoinsSelectedEvent;
    private int amountOfMoneyWon;
    private int countDeposit;

    private void Start()
    {
        casinoView.OnClickMinimumRateButton += GetMinimumRateMoney;
        casinoView.OnClickAverageRateButton += GetAvarageRateMoney;
        casinoView.OnClickMaximumRateButton += GetMaximumRateMoney;

        worldTime.OnStopCasinoEvent += CheckingProbabilityWinningMoney;
    }

    private void GetMinimumRateMoney()
    {
        if (countDeposit < 1)
        {
            marketController.GetMoney(5);

            countCoinsSelectedEvent = 5;

            countDeposit++;
        }
    }

    private void GetAvarageRateMoney()
    {
        if (countDeposit < 1)
        {
            marketController.GetMoney(10);

            countCoinsSelectedEvent = 10;

            countDeposit++;
        }
    }

    private void GetMaximumRateMoney()
    {
        if (countDeposit < 1)
        {
            marketController.GetMoney(15);

            countCoinsSelectedEvent = 15;

            countDeposit++;
        }
    }

    private void CheckingProbabilityWinningMoney()
    {
        if (marketController.CalculateProbabilityWinning() == false)
        {
            UIController.ShowInfo("Вы програли свою ставку!", "Ок");
        }
        else
        {
            marketController.CalculateWinningAmount(countCoinsSelectedEvent, out amountOfMoneyWon);

            marketController.SetWinMoney(amountOfMoneyWon);

            UIController.ShowInfo($"Ваша ставка выиграла! Вы получаете дополнительно {amountOfMoneyWon} монет!", "Ок");
        }
    }
}
