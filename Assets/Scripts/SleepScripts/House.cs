using Ekonomika.Utils;
using UnityEngine.SceneManagement;

public class House : ClickableObject
{
    public override void Execute(Character player)
    {
        UIController.ShowYesNoDialog("Вы хотите войти в свой дом?", () =>
        {
            CameraSwitch.SwichHouseCamera();
        });
    }
}
