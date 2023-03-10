using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public Button buttonChange;
    public TMP_Text buttonChangeText;
    public Button buttonInfo;
    public Image avatarCharacter;
    [HideInInspector] public GameObject prefabCharacter;
    [HideInInspector] public CharacterSO characterSO;
     
    public void GetCharacter()
    {
        MenuManager.instance.GetCharacter(this);
    }

    public void OpenInfo()
    {
        MenuManager.instance.OpenCharacterInfo(this);
    }
}
