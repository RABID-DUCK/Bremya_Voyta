using UnityEngine.SceneManagement;

public class House : ClickableObject
{
    public override void Execute(Character player)
    {
        UIController.ShowYesNoDialog("Вы хотите войти в свой дом?", () =>
        {
            SceneManager.LoadScene(3);
        });
    }
}
