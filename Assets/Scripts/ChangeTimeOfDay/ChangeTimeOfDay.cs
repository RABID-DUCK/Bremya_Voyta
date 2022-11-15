using System.Drawing;
using UnityEngine;

public class ChangeTimeOfDay : MonoBehaviour
{
    [SerializeField] private Light directionalLight;

    [Header("Time of day color settings")]
    [Tooltip("Gradient of the day")]
    [SerializeField] private Gradient directionalLightGradient;
    [Tooltip("Gradient of the night")]
    [SerializeField] private Gradient ambientLightGradient;

    [Header("Time of day settings")]
    [Tooltip("Number of seconds in one day")]
    [Range(0, 260)] public float timeDayInSeconds;
    [Tooltip("Time of day range")]
    [Range(0f, 1f)] public float timeProgress;

    [Space, Tooltip("Count of days elapsed")]
    public int countOfDaysElapsed;

    private Vector3 defaultAngles;

    private void Start()
    {
        defaultAngles = directionalLight.transform.localEulerAngles;
    }

    private void Update()
    {
        if(timeDayInSeconds != 0)
        {
            print("There can't be 0 seconds in one day!!!");
        }

        if (Application.isPlaying)
        {
            timeProgress += Time.deltaTime / timeDayInSeconds;
        }

        if (timeProgress > 1f)
        {
            timeProgress = 0f;

            countOfDaysElapsed++;
        }

        directionalLight.color = directionalLightGradient.Evaluate(timeProgress);

        RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);

        directionalLight.transform.localEulerAngles = new Vector3(360f * timeProgress - 90,
            defaultAngles.x, defaultAngles.z);
    }
}
