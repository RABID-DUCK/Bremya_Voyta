using Ekonomika.Utils;
using Photon.Pun;
using UnityEngine;

public class House : OutlineClickableObject
{
    public string profession;
    public int skin;
    public string playerNick;
    public Transform doorway;

    public override void OnExecute()
    {
        if (Player.photonView.Controller.NickName == playerNick)
        {
            UIController.ShowYesNoDialog("Вы хотите войти в свой дом?", () =>
            {
                Player.Teleport(new Vector3(1f, -49.893f, 0.23f));
                CameraSwitch.SwichHouseCamera();
                Player.SetMovement(false);
                Player.SetVisibleOtherPlayers(false);
            });
        }
        else
        {
            UIController.ShowOkInfo("Вы не можете войти в данный дом.\n" +
                (string.IsNullOrEmpty(playerNick) ? "Дом заброшен." : 
                                                   $"Дом принадлежит:\n<b>{playerNick}</b>"));
        }
    }
}
