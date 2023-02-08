using Ekonomika.Utils;
using UnityEngine;

public class House : ClickableObject
{
    public string profession;
    public int skin;
    public string playerNick;
    public Transform doorway;

    public override void Execute(Character player)
    {
        if (player.photonView.Controller.NickName == playerNick)
        {
            UIController.ShowYesNoDialog("Вы хотите войти в свой дом?", () =>
            {
                player.Teleport(new Vector3(1f, -49.3f, 0.23f));
                CameraSwitch.SwichHouseCamera();
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
