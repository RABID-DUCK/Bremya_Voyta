using Ekonomika.Utils;

public class ExitRoom : ClickableObject
{
    public override void Execute(Character player)
    {
        UIController.ShowYesNoDialog("�� ������ ����� �� ����?", () =>
        {
            CameraSwitch.SwichToMainCamera();
        });
    }
}
