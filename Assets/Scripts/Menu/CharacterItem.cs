using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public Button buttonChange;
    public Button buttonInfo;
    public Image avatarCharacter;
    [HideInInspector] public LobbyMenu lm;
    public CharacterSO characterSO;

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();

    public void SetInfo()
    {
        
    }

    public void ChangeCharacter()
    {
        _CP["Profession"] = characterSO.nameCharacter;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);

        lm.Send_Data("ReloadCharacter", PhotonNetwork.LocalPlayer.ActorNumber);
    }
}
