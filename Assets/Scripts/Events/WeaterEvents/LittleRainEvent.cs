using Ekonomika.Work;
using System.Collections;
using UnityEngine;

public class LittleRainEvent : MonoBehaviour
{
    [Header("Rain settings")]
    [Tooltip("Scriptable event object \"Little RainSO\"")]
    public EventSO littleRainSO;

    [Space, Tooltip("Rain Particle System")]
    [SerializeField] private GameObject littleRainPS;
    [Tooltip("General Directional light")]
    [SerializeField] private Light directionalLight;

    //TODO: ����� �������� ���� �����

    public void StartSmallRainEvent() // ���� ����� ����� ��������, ��� ������ �������!!!
    {
        StartCoroutine(LightDarkens());

        littleRainPS.SetActive(true);

        StartLittleRainEffect();
    }

    private void StartLittleRainEffect()
    {
        WorkOverrider.OverrideFarmerDropItems(6, 8);

        //TODO: ������ + 3 �� ����. ���-��
    }

    public void EndSmallRainEvent() // ���� ����� ����� ��������, ��� ����� �������!!!
    {
        // StartCoroutine(LightIsBrighter());

        littleRainPS.SetActive(false);

        EndlittleRainEffect();
    }

    private void EndlittleRainEffect()
    {
        WorkOverrider.ReturnFarmerDropItems();

        //TODO: ������� � �����
    }

    private IEnumerator LightDarkens()
    {
        for (float i = 1f; i == 0.5f; i -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);

            directionalLight.intensity = i;
        }
    }

    private IEnumerator LightIsBrighter()
    {
        for (float i = 0.5f; i < 1; i += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);

            directionalLight.intensity = i;
        }
    }
}
