using Ekonomika.Utils;
using UnityEngine;

public class ExitRoom : ClickableObject
{
    public override void Execute(Character player)
    {
        UIController.ShowYesNoDialog("�� ������ ����� �� ����?", () =>
        {
            player.ReturnHome();
            CameraSwitch.SwichToMainCamera();
            player.gameObject.GetComponent<PlayerController>().enabled = true;
        });
    }
}
