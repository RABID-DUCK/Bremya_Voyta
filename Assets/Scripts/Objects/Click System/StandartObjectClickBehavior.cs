using Ekonomika.Work;
using UnityEngine;

public class StandartObjectClickBehavior : MonoBehaviour, IObjectClickBehavior
{
    private Character _player;

    public void OnObjectClick(IClickableObject clickableObject)
    {
        if (clickableObject is IWork)
        {
            if (!CheckCompatibilityWork((IWork)clickableObject))
                return;
        }

        if (clickableObject.Enabled)
        {
            clickableObject.Execute();
        }
    }

    public bool CheckCompatibilityWork(IWork work)
    {
        bool compatibility = _player.Type == work.WorkerType;
        
        if (!compatibility)
            UIController.ShowOkInfo($"�� �� ������ �������� �� ������ ������! \n���������: <b>{work.WorkerName}</b>.");

        return compatibility;
    }

    public void SetPlayer(Character player)
    {
        _player = player;
    }
}
