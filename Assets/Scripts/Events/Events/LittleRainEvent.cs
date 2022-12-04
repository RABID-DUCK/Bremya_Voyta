using UnityEngine;

public class LittleRainEvent : MonoBehaviour
{
    [Header("Rain settings")]

    public EventSO smallRainSO;

    [SerializeField] private GameObject smallRainPS;

    [SerializeField] private Light directionalLight;

    [SerializeField] private Light nightSpotLight; // ��� ����� ������ ���� ������ ���� �������, ��� ������!!!

    //TODO: ����� �������� ���� �����

    public void StartSmallRain() // ���� ����� ����� ��������, ��� ������ �������!!!
    {
        smallRainPS.SetActive(true);

        EffectOfEvent();
    }

    public void EndSmallRain() // ���� ����� ����� ��������, ��� ����� �������!!!
    {
        smallRainPS.SetActive(false);

        RemoteEffectOfEvent();
    }

    public void EffectOfEvent() // ����������� ������ ������� �� ������!!!
    {
        
    }

    public void RemoteEffectOfEvent() // ����������� ������ ������ �������!!!
    {

    }
}
