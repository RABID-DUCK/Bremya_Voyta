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
                player.Teleport(new Vector3(1f, -49.3f, 0.23f));
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
