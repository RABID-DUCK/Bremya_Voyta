using Unity.VisualScripting;
using UnityEngine;

public class ThunderstormWithHeavyRainEvent : MonoBehaviour
{
    [SerializeField] private WorldTime changeTimeOfDay;

    [Tooltip("The object of the particle system")]
    [SerializeField] private GameObject ThunderstormPS;

    [Tooltip("Light from Lightning")]
    [SerializeField] private Light lightThunder;

    [Range(0,100), Tooltip("the amount of time in which the sound" +
        "and light from the lightning will be reflected")]
    [SerializeField] private float thunderTime;

    [SerializeField] private Light nightSpotLight; // При грозе данный свет должен быть тусклее, чем обычно!!!

    //TODO: Нужно добавить звук ливня и грозы

    private void Update()
    {
        thunderTime += Time.deltaTime;
    }

    public void StartSmallRain() // Этот метод нужно вызывать, при старте события!!!
    {
        ThunderstormPS.SetActive(true);

        EffectOfEvent();
    }

    public void EndSmallRain() // Этот метод нужно вызывать, при конце события!!!
    {
        ThunderstormPS.SetActive(false);

        RemoteEffectOfEvent();
    }

    public void CalcStartThunder() // Этот метод нужен для появления молнии.
    {
        int randomStartNum = Random.Range(1, 100);

        if(thunderTime == randomStartNum)
        {
            Starthunder();

            thunderTime = 0;
        }
    }

    private void Starthunder() // Метод отвечающий за появление молнии.
    {

    }

    public void EffectOfEvent() // Реализовать логику эффекта от ивента.
    {

    }

    public void RemoteEffectOfEvent() // Реализовать логику снятия эффекта.
    {

    }
}
