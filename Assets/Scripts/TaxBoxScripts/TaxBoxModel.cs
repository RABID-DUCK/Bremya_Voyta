using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TaxBoxModel : MonoBehaviourPunCallbacks
{
    [Header("Read only!")]
    public List<Item> selectResources = new List<Item>();
    public List<int> selectCountResources = new List<int>();

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
            resurces.RemoveAt(selectIndex);

            Randomize(1, 2, out selectResourceCount);

            selectCountResources.Add(selectResourceCount);
        }
    }

    public void SetSelectedResurcesInformationOnTaxBoxPanel(List<TaxBoxViewItem> taxBoxItems)
    {
        if (selectResources.Count == 3 && selectCountResources.Count == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                taxBoxItems[i].image.sprite = selectResources[i].ItemSprite;
                taxBoxItems[i].nameText.text = selectResources[i].ItemName;
                taxBoxItems[i].countText.text = $"Налог составляет: {selectCountResources[i]}шт.";
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
                Debug.Log($"Выбранные ресурс {i} {selectResources[i].name}");
                Debug.Log($"Количество ресурса {selectCountResources[i]}");

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
            UIController.ShowInfo("Как можно быть настолько бедным?\r\nМожет пора поработать?", "Ок");
        }
    }
}