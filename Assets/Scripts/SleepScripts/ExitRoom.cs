using Ekonomika.Utils;

public class ExitRoom : OutlineClickableObject
{
    protected override void OnExecute()
    {
        UIController.ShowYesNoDialog("Вы хотите выйти из дома?", () =>
        {
            Player.ReturnHome();
            CameraSwitch.SwichToMainCamera();
            Player.SetMovement(true);
            Player.SetVisibleOtherPlayers(true);
        });
    }
}
