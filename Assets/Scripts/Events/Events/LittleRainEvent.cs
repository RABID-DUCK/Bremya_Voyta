using UnityEngine;

public class LittleRainEvent : MonoBehaviour
{
    [Header("Rain settings")]

    public EventSO smallRainSO;

    [SerializeField] private GameObject smallRainPS;

    [SerializeField] private Light nightSpotLight; // ��� ����� ������ ���� ������ ���� �������, ��� ������!!!

    [Header("Day and night settings when it rains")]
    [Tooltip("Gradient of the day")]
    [SerializeField] private Gradient directionalLightGradient;
    [Tooltip("Gradient of the night")]
    [SerializeField] private Gradient ambientLightGradient;

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
