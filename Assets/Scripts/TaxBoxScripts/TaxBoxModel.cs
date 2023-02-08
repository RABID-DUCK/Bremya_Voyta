using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxBoxModel : MonoBehaviour
{
    [HideInInspector] public List<Item> selectResources = new List<Item>();
    [HideInInspector] public List<int> selectCountResources = new List<int>();

    public event Action OnPaymentError;

    private void Randomize(int firstCount, int secondCount, out int selectIndex)
    {
        System.Random rnd = new System.Random();

        selectIndex = rnd.Next(firstCount, secondCount);
    }

    public void SelectRandomResurses(List<Item> resurces)
    {
        for (int i = 0; i < 3; i++)
        {
            int selectIndex = 0;
            int selectResourceCount = 0;

            Randomize(0, resurces.Count, out selectIndex);

            selectResources.Add(resurces[selectIndex]);
            resurces.RemoveAt(selectIndex); //Костыль привет!

            Randomize(1, 2, out selectResourceCount);

            selectCountResources.Add(selectResourceCount);
        }
    }

    public void SetSelectedResurcesInformationOnTaxBoxPanel(List<Image> imageResuces, List<TMP_Text> nameResurcesText, List<TMP_Text> countResurcesText)
    {
        if (selectResources.Count == 3 && selectCountResources.Count == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                imageResuces[i].sprite = selectResources[i].ItemSprite;
                nameResurcesText[i].text = selectResources[i].ItemName;
                countResurcesText[i].text = $"Налог составляет: {selectCountResources[i]}шт.";
            }
        }
        else
        {
            Debug.LogError($"Ты еблан. У тебя в массивах больше 3-х элементов\r\n" +
                $"selectResources = {selectResources.Count}; selectCountResources = {selectCountResources.Count}");
        }
    }

    public bool GetResourcesFromPlayer(Character player)
    {
        try
        {
            for (int i = 0; i < selectResources.Count; i++)
            {
                player.PlayerInventory.PickUpItem(selectResources[i], selectCountResources[i]);
            }

            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    public void TakePenaltyForNonPaymentOfTax(Character player)
    {
        try
        {
            player.PlayerWallet.PickUpCoins(10);

        }
        catch (InvalidOperationException)
        {
            UIController.ShowInfo("Ну грусто... Что сказать?...", "Ок =(");
        }
    }
}