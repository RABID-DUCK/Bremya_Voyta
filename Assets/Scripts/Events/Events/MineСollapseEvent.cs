using UnityEngine;

public class MineСollapseEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"MineСollapseSO\"")]
    public EventSO MineСollapseSO;

    public void StartMineСollapseEvent() // Этот метод нужно вызывать, при старте события!!!
    {
        StartMineСollapseEffect();
    }

    private void StartMineСollapseEffect()
    {
        // TODO: копатель, - 2шт. от макс. кол-ва.
    }

    public void EndMineСollapseEvent() // Этот метод нужно вызывать, при конце события!!!
    {
        EndMineСollapseEffect();
    }

    private void EndMineСollapseEffect()
    {
        // TODO: Вернуть назад
    }
}
