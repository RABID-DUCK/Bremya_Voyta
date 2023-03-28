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
            UIController.ShowYesNoDialog("�� ������ ����� � ���� ���?", () =>
            {
                Player.Teleport(new Vector3(1f, -49.893f, 0.23f));
                CameraSwitch.SwichHouseCamera();
                Player.SetMovement(false);
                Player.SetVisibleOtherPlayers(false);
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
