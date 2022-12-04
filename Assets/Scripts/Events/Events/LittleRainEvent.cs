using UnityEngine;

public class LittleRainEvent : MonoBehaviour
{
    [Header("Rain settings")]

    public EventSO smallRainSO;

    [SerializeField] private GameObject smallRainPS;

    [SerializeField] private Light nightSpotLight; // При дожде данный свет должен быть тусклее, чем обычно!!!

    [Header("Day and night settings when it rains")]
    [Tooltip("Gradient of the day")]
    [SerializeField] private Gradient directionalLightGradient;
    [Tooltip("Gradient of the night")]
    [SerializeField] private Gradient ambientLightGradient;

    //TODO: Нужно добавить звук дождя

    public void StartSmallRain() // Этот метод нужно вызывать, при старте события!!!
    {
        smallRainPS.SetActive(true);

        EffectOfEvent();
    }

    public void EndSmallRain() // Этот метод нужно вызывать, при конце события!!!
    {
        smallRainPS.SetActive(false);

        RemoteEffectOfEvent();
    }

    public void EffectOfEvent() // Реализовать логику эффекта от ивента!!!
    {

    }

    public void RemoteEffectOfEvent() // Реализовать логику снятия эффекта!!!
    {

    }
}
