using System;
using UnityEngine;

public class TaxBox : ClickableObject
{
    [SerializeField] private GameObject taxBox;

    public event Action OnClickTaxBox;

    public override void Execute(Character player)
    {
        OnClickTaxBox?.Invoke();
    }
}