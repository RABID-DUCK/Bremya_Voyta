using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MarketPlace : MonoBehaviour
{
    [SerializeField] public Character _charackter;

    [SerializeField] public Button BuyMilk;
    [SerializeField] public Button BuyWood;
    [SerializeField] public Button BuyMetall;
    [SerializeField] public Button BuyMeat;
    [SerializeField] public Button BuyFish;
    [SerializeField] public Button BuyCorrat;

    [HideInInspector] public int btn = 0;
    public event Action<int> selectedButton;


    void Start()
    {
        BuyMilk.onClick.AddListener(SelectedMilkBtn);
        BuyWood.onClick.AddListener(SelectedWoodBtn);
        BuyMetall.onClick.AddListener(SelectedMetallBtn);
        BuyMeat.onClick.AddListener(SelectedMeatBtn);
        BuyFish.onClick.AddListener(SelectedFishBtn);
        BuyCorrat.onClick.AddListener(SelectedCorratBtn);
    }

    public void SelectedMilkBtn()
    {
        
        selectedButton?.Invoke(btn);
    }

    //public void AsychMoney()
    //{
    //    if (_charackter.inventory.coins < 15)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        _charackter.inventory.item += 1;
    //    }
    //}

    public void SelectedWoodBtn()
    {
        if (_charackter.inventory.coins < 15)
        {
            return;
        }
        else
        {
            _charackter.inventory.woodCount +=1;
        }
        selectedButton?.Invoke(btn);
    }

    public void SelectedMetallBtn()
    {
        if (_charackter.inventory.coins < 15)
        {
            return;
        }
        else
        {
            _charackter.inventory.metallCount += 1;
        }
        selectedButton?.Invoke(btn);
    }

    public void SelectedMeatBtn()
    {
        if (_charackter.inventory.coins < 15)
        {
            return;
        }
        else
        {
            _charackter.inventory.meatCount += 1;
        }
        selectedButton?.Invoke(btn);
    }

    public void SelectedFishBtn()
    {
        if (_charackter.inventory.coins < 15)
        {
            return;
        }
        else
        {
            _charackter.inventory.fishCount += 1;
        }
        selectedButton?.Invoke(btn);
    }

    public void SelectedCorratBtn()
    {
        if (_charackter.inventory.coins < 15)
        {
            return;
        }
        else
        {
            _charackter.inventory.corratCount += 1;
        }
        selectedButton?.Invoke(btn);
    }
}
