using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Serializable]
    public struct CharacterInventory
    {
        public int coins;

        public int woodCount;
        public int berriesCount;
        public int carrotCount;
        public int milkCount;
        public int coalCount;
        public int ironCount;
        public int meatCount;
        public int fishCount;
    }

    public CharacterInventory inventory;

    private List<IProfession> professions = new List<IProfession>();
    private ClickEventer clickEventer;

    public bool Init { get; private set; }

    private void Start()
    {
        GetComponents(professions);
    }

    private void OnDestroy()
    {
        if (Init)
        {
            clickEventer.OnClickWork -= GetObject;
        }
    }

    public void Initialization(ClickEventer clickEventer)
    {
        print(clickEventer);
        OnDestroy();
        InitPlayer(clickEventer);
    }

    private void InitPlayer(ClickEventer clickEventer)
    {
        clickEventer.OnClickWork += GetObject;
        this.clickEventer = clickEventer;

        Init = true;
    }

    private void GetObject(IWork clickableObject)
    {
        bool checkWork = true;
        foreach (IProfession profession in professions)
        {
            if (profession.CheckProfessionObject(clickableObject))
            {
                // TODO: Add work dialog window.

                clickableObject.Execute(this);
                checkWork = false;
                break;
            }
        }

        if (checkWork)
        {
            // TODO: Add warning label.
        }
    }
}
