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
            UIController.ShowYesNoDialog("�� ������ ����� � ���� ���?", () =>
            {
                CameraSwitch.SwichHouseCamera();
            });
        }
        else
        {
            UIController.ShowOkInfo("�� �� ������ ����� � ������ ���.\n" +
                (string.IsNullOrEmpty(playerNick) ? "��� ��������." : 
                                                   $"��� �����������:\n<b>{playerNick}</b>"));
        }
    }
}
