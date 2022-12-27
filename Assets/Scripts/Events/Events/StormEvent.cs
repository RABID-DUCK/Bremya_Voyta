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
        //TODO: Лес, -2шт. от макс. кол-ва.
    }

    private void EndStormEffect()
    {
        //TODO: Веруть на мэсто
    }
}
