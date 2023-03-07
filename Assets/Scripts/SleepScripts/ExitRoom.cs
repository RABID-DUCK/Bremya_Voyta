using Ekonomika.Utils;

public class ExitRoom : OutlineClickableObject
{
    public override void Execute()
    {
        UIController.ShowYesNoDialog("Вы хотите выйти из дома?", () =>
        {
            Player.ReturnHome();
            CameraSwitch.SwichToMainCamera();
            Player.gameObject.GetComponent<PlayerController>().enabled = true;
        });
    }
}
