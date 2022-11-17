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

    [SerializeField] private Light nightSpotLight; // ��� ����� ������ ���� ������ ���� �������, ��� ������!!!

    //TODO: ����� �������� ���� ����� � �����

    private void Update()
    {
        thunderTime += Time.deltaTime;
    }

    public void StartSmallRain() // ���� ����� ����� ��������, ��� ������ �������!!!
    {
        ThunderstormPS.SetActive(true);

        EffectOfEvent();
    }

    public void EndSmallRain() // ���� ����� ����� ��������, ��� ����� �������!!!
    {
        ThunderstormPS.SetActive(false);

        RemoteEffectOfEvent();
    }

    public void CalcStartThunder() // ���� ����� ����� ��� ��������� ������.
    {
        int randomStartNum = Random.Range(1, 100);

        if(thunderTime == randomStartNum)
        {
            Starthunder();

            thunderTime = 0;
        }
    }

    private void Starthunder() // ����� ���������� �� ��������� ������.
    {

    }

    public void EffectOfEvent() // ����������� ������ ������� �� ������.
    {

    }

    public void RemoteEffectOfEvent() // ����������� ������ ������ �������.
    {

    }
}
