using System;
using UnityEngine;

public class TaxBox : OutlineClickableObject
{
    [SerializeField] private GameObject taxBox;

    public event Action OnClickTaxBox;

    public override void Execute()
    {
        OnClickTaxBox?.Invoke();
    }
}