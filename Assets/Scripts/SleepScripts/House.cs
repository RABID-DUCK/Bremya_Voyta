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
        UIController.ShowYesNoDialog("�� ������ ����� � ���� ���?", () =>
        {
            CameraSwitch.SwichHouseCamera();
        });
    }
}
