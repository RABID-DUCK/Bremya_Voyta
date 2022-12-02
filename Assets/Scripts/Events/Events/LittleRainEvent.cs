using UnityEngine;

public class LittleRainEvent : MonoBehaviour
{
    [SerializeField] private GameObject smallRainPS;

    [SerializeField] private Light directionalLight;

    [SerializeField] private Light nightSpotLight; // При дожде данный свет должен быть тусклее, чем обычно!!!

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
