using UnityEngine.SceneManagement;

public class House : ClickableObject
{
    public override void Execute(Character player)
    {
        UIController.ShowYesNoDialog("���", "�� ������ ����� � ���� ���?", () =>
        {
            SceneManager.LoadScene(3);
        });
    }
}
