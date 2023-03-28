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

        clickableObject.Execute();
    }

    public bool CheckCompatibilityWork(IWork work)
    {
        bool compatibility = _player.Type == work.WorkerType;
        
        if (!compatibility && work.Enabled)
            UIController.ShowOkInfo($"Вы не можете работать на данной работе! \nТребуется: <b>{work.WorkerName}</b>.");

        return compatibility;
    }

    public void SetPlayer(Character player)
    {
        _player = player;
    }
}
