using System;
using UnityEngine;

public class TaxBox : OutlineClickableObject
{
    [SerializeField] private GameObject taxBox;

    public event Action OnClickTaxBox;

    public AudioClip taxBoxClip;

    public AudioSource iventsSounds;

    public override void OnExecute()
    {
        OnClickTaxBox?.Invoke();

        iventsSounds.clip = taxBoxClip;
        iventsSounds.Play();
    }
}