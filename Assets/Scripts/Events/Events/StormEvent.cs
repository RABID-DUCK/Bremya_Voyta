using UnityEngine;

public class StormEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"StormSO\"")]
    public EventSO StormSO;

    [SerializeField] private WorkController workController;
 
    public void StartStormEvent()
    {
        StartStormEffect();
    }

    public void EndStormEvent()
    {
        EndStormEffect();
    }

    private void StartStormEffect()
    {
        workController.OvverideLumberjackDropItems(1, 3);

        //TODO: ���, -2��. �� ����. ���-��.
    }

    private void EndStormEffect()
    {
        workController.ReturnIronMiningDropItems();

        //TODO: ������ �� �����
    }
}
