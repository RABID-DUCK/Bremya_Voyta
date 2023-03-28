using Ekonomika.Utils;

public class ExitRoom : OutlineClickableObject
{
    public override void OnExecute()
    {
        UIController.ShowYesNoDialog("�� ������ ����� �� ����?", () =>
        {
            Player.ReturnHome();
            CameraSwitch.SwichToMainCamera();
            Player.SetMovement(true);
        });
    }
}
