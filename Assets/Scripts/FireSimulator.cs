using System.Collections;
using UnityEngine;

public class FireSimulator : MonoBehaviour
{
    [Tooltip("Light component")]
    [SerializeField] private Light pointLight;

    [Header("Light attenuation range")]
    [Tooltip("This value should not be less than 0")]
    [SerializeField] private float minAttenuationValue = 0.2f;
    [Tooltip("This value should not exceed 1.5!")]
    [SerializeField] private float maxAttenuationValue = 1f;

    [Space, Tooltip("The speed at which the light bulb fades or brightens")]
    [SerializeField] private float speedAttenuationValue = 0.05f;

    private float timerAttenuation;

    private float startIntensityValue;

    private bool isCheckBug = false;

    private void Start()
    {
        if (pointLight != null)
        {
            startIntensityValue = pointLight.intensity;
        }
        else
        {
            pointLight = GetComponent<Light>();

            startIntensityValue = pointLight.intensity;
        }

        if (minAttenuationValue >= startIntensityValue)
        {
            Debug.LogError($"Минимальное значение, в диапазоне интенсивности света, не должно быть больше или равно стартовому");

            isCheckBug = true;
        }

        if (maxAttenuationValue > startIntensityValue - 1)
        {
            Debug.LogError($"Минимальное значение, в диапазоне интенсивности света, не должно быть меньше стартового, чем на 1!");

            isCheckBug = true;
        }

        timerAttenuation = GetRandomValue(8, 15);
    }

    private void Update()
    {
        if (timerAttenuation <= 0 && isCheckBug == false)
        {
            FireSimulation(pointLight);

            timerAttenuation = GetRandomValue(8, 15);
        }
        else if (timerAttenuation > 0 && isCheckBug == false)
        {
            timerAttenuation -= (Time.deltaTime);
        }
    }

    [ContextMenu("Light off")]
    public void LightOff()
    {
        StartCoroutine(LightOffCourotine(pointLight));
    }

    [ContextMenu("Light on")]
    public void LightOn()
    {
        StartCoroutine(LightOnCourotine(pointLight));
    }

    private void FireSimulation(Light pointLight)
    {
        StartCoroutine(Attenuation(pointLight, GetRandomValue(minAttenuationValue, maxAttenuationValue)));
    }

    private float GetRandomValue(float minValue, float maxValue)
    {
        float currentIntensity = Random.Range(minValue, maxValue);

        return currentIntensity;
    }

    private IEnumerator Attenuation(Light pointLight, float secondIntensityValue)
    {
        int countAttenuation = (int)GetRandomValue(1, 3);

        while (countAttenuation != 0)
        {
            for (float i = startIntensityValue; i >= secondIntensityValue; i -= speedAttenuationValue)
            {
                pointLight.intensity -= speedAttenuationValue;

                yield return new WaitForEndOfFrame();
            }
            pointLight.intensity = secondIntensityValue;

            for (float i = secondIntensityValue; i < startIntensityValue; i += speedAttenuationValue)
            {
                pointLight.intensity += speedAttenuationValue;

                yield return new WaitForEndOfFrame();
            }
            pointLight.intensity = startIntensityValue;

            countAttenuation -= 1;
        }
    }

    private IEnumerator LightOffCourotine(Light pointLight)
    {
        for (float i = startIntensityValue; i >= 0; i -= speedAttenuationValue)
        {
            pointLight.intensity -= speedAttenuationValue;

            yield return new WaitForEndOfFrame();
        }

        pointLight.intensity = 0;
    }

    private IEnumerator LightOnCourotine(Light pointLight)
    {
        for (float i = 0; i < startIntensityValue; i += speedAttenuationValue)
        {
            pointLight.intensity += speedAttenuationValue;

            yield return new WaitForEndOfFrame();
        }

        pointLight.intensity = startIntensityValue;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
