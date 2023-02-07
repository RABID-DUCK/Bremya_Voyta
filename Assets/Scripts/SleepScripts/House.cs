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
