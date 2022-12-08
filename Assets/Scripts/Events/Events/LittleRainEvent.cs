using UnityEngine;

public class LittleRainEvent : MonoBehaviour
{
    [Header("Rain settings")]
    [Tooltip("Scriptable event object \"Little Rain\"")]
    public EventSO littleRainSO;
    [Tooltip("Rain Particle System")]
    [SerializeField] private GameObject littleRainPS;

    //TODO: Ќужно добавить звук дожд€

    public void StartSmallRain() // Ётот метод нужно вызывать, при старте событи€!!!
    {
        littleRainPS.SetActive(true);
    }

    public void EndSmallRain() // Ётот метод нужно вызывать, при конце событи€!!!
    {
        littleRainPS.SetActive(false);
    }
}
