using UnityEngine;

public class StormEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"StormSO\"")]
    public EventSO StormSO;

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
        //TODO: ���, -2��. �� ����. ���-��.
    }

    private void EndStormEffect()
    {
        //TODO: ������ �� �����
    }
}
