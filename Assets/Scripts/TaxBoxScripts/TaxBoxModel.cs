using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxBoxModel : MonoBehaviour
{
    [HideInInspector] public List<Item> selectResources = new List<Item>();
    [HideInInspector] public List<int> selectCountResources = new List<int>();

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

            Randomize(1, 2, out selectResourceCount);

            selectCountResources.Add(selectResourceCount);
        }
    }

    public void SetSelectedResurcesInformationOnTaxBoxPanel(List<Image> imageResuces, List<TMP_Text> nameResurcesText, List<TMP_Text> countResurcesText)
    {
        for (int i = 0; i < 3; i++)
        {
            imageResuces[i].sprite = selectResources[i].ItemSprite;
            nameResurcesText[i].text = selectResources[i].ItemName;
            countResurcesText[i].text = $"Налог составляет: {selectCountResources[i]}шт.";
        }
    }

    public void GetResourcesFromPlayer()
    {
        //TODO: Дописать
    }

    public bool CheckingResources()
    {
        return true; //TODO: Дописать
    }
}