using Ekonomika.Utils;
using System;
using UnityEngine;

public class MarketChest : OutlineClickableObject
{
    public Action<Inventory> OnOpenChes;

    public AudioClip marketChestClip;

    public AudioSource iventsSounds;

    public override void OnExecute()
    {
        OnOpenChes?.Invoke(Player.PlayerInventory);

        iventsSounds.clip = marketChestClip;
        iventsSounds.Play();
    }
}
