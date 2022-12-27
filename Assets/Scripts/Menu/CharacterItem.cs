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
    [HideInInspector] public LobbyMenu lm;
    [HideInInspector] public CharacterSO characterSO;

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();

    public void ChangeCharacter()
    {
        foreach (var (_nick, j) in characterSO.full.Select((_nick, j) => (_nick, j)))
        {
            if (characterSO.full[j] == "")
            {
                _CP["Profession"] = $"{characterSO.nameCharacter}|{j}";
                PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);
                break;
            }
        }
    }

    public void OpenInfo()
    {
        lm.characterInfoImage.sprite = characterSO.avatars[0];
        lm.characterInfoText.text = characterSO.description;
        lm.characterInfoPanel.SetActive(true);
    }
}
