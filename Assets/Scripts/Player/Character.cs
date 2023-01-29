using Ekonomika.Utils;
using Ekonomika.Work;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Wallet PlayerWallet { get; private set; } = new Wallet();
    public Inventory PlayerInventory { get; private set; } = new Inventory();
    
    public bool Init { get; private set; }

    private ClickEventer clickEventer;

    [SerializeField]
    private List<Item> allowedEarnedItems;

    private void OnDestroy()
    {
        if (Init)
        {
            clickEventer.OnClickWork -= GetObject;
        }
    }

    public void Initialization(ClickEventer clickEventer)
    {
        OnDestroy();
        InitPlayer(clickEventer);
    }

    private void InitPlayer(ClickEventer clickEventer)
    {
        clickEventer.OnClickWork += GetObject;
        this.clickEventer = clickEventer;

        Init = true;
    }

    private void GetObject(IWork workObject)
    {
        bool checkWork = allowedEarnedItems.Find(x => { return x == workObject.ReceivedItem; });

        if (checkWork)
        {
            workObject.Execute(this);
        }
        else
        {
            UIController.ShowOkInfo($"¬ы не можете работать на данной работе ({workObject.WorkName})!");
        }
    }
}
