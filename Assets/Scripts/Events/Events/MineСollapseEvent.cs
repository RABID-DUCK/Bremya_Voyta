using UnityEngine;

public class MineСollapseEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"MineСollapseSO\"")]
    public EventSO MineСollapseSO;

    [SerializeField] private WorkController workController;

    public void StartMineСollapseEvent() // Этот метод нужно вызывать, при старте события!!!
    {
        StartMineСollapseEffect();
    }

    private void StartMineСollapseEffect()
    {
        workController.OvverideCoalMiningDropItems(1, 3);
        workController.OvverideIronMiningDropItems(1, 3);

        // TODO: копатель, - 2шт. от макс. кол-ва.
    }

    public void EndMineСollapseEvent() // Этот метод нужно вызывать, при конце события!!!
    {
        EndMineСollapseEffect();
    }

    private void EndMineСollapseEffect()
    {
        workController.ReturnCoalMiningDropItems();
        workController.ReturnIronMiningDropItems();

        // TODO: Вернуть назад
    }
}
