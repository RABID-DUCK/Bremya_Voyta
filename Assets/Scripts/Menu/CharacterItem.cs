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
    [HideInInspector] public LobbyMenu lm;
    [HideInInspector] public CharacterSO characterSO;

    private Hashtable _CP = new Hashtable();

    public void ChangeCharacter()
    {
        foreach (var (_nick, j) in characterSO.full.Select((_nick, j) => (_nick, j)))
        {
            if (characterSO.full[j] == "" || characterSO.full[j] == PhotonNetwork.LocalPlayer.NickName)
            {
                _CP["Profession"] = $"{characterSO.nameCharacter}";
                _CP["Skin"] = j;
                PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);
                lm.Room.SetActive(true);
                lm.charactersMenu.SetActive(false);
                return;
            }
        }
        lm.ErrorText.text = $"Такая профессия уже занята";
        lm.Error.SetActive(true);
    }

    public void OpenInfo()
    {
        lm.characterInfoImage.sprite = characterSO.avatars[0];
        lm.characterInfoText.text = characterSO.description;
        lm.characterInfoPanel.SetActive(true);
    }
}
