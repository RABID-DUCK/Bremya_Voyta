using UnityEngine;

public class LittleRainEvent : MonoBehaviour
{
    [Header("Rain settings")]
    [Tooltip("Scriptable event object \"Little Rain\"")]
    public EventSO littleRainSO;
    [Tooltip("Rain Particle System")]
    [SerializeField] private GameObject littleRainPS;

    //TODO: ����� �������� ���� �����

    public void StartSmallRain() // ���� ����� ����� ��������, ��� ������ �������!!!
    {
        littleRainPS.SetActive(true);
    }

    public void EndSmallRain() // ���� ����� ����� ��������, ��� ����� �������!!!
    {
        littleRainPS.SetActive(false);
    }
}
