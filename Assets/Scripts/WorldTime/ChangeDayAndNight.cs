using UnityEngine;

public class ChangeDayAndNight : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;

    [Tooltip("Directional light")]
    [SerializeField] private Light directionalLight;

    [Header("Gradient time of day")]
    [Tooltip("Gradient of light at day")]
    public Gradient dayGradient;

    [Tooltip("Gradient of light at night")]
    public Gradient nightGradient;

    [Header("Sky gradient settings")]
    [Tooltip("A gradient that colors the sky of day")]
    public Gradient daySkyGradient;

    [Tooltip("A gradient that colors the sky of day")]
    public Gradient nightSkyGradient;

    private Vector3 defaultAngles;

    private void Start()
    {
        defaultAngles = directionalLight.transform.localEulerAngles;
    }

    private void Update()
    {
        if (worldTime.CheckTimeOfDay)
        {
            ChangingGradientColorDay();

            directionalLight.intensity = 1f;
        }
        else
        {
            ChangingGradientColorNught();

            directionalLight.intensity = 0.2f;
        }
    }

    private void ChangingGradientColorDay()
    {
        directionalLight.color = dayGradient.Evaluate(worldTime.timeProgress);

        RenderSettings.ambientLight = daySkyGradient.Evaluate(worldTime.timeProgress);

        directionalLight.transform.localEulerAngles = new Vector3(180 * worldTime.timeProgress,
            defaultAngles.x, defaultAngles.z);
    }

    private void ChangingGradientColorNught()
    {
        directionalLight.color = nightGradient.Evaluate(worldTime.timeProgress);

        RenderSettings.ambientLight = nightSkyGradient.Evaluate(worldTime.timeProgress);

        directionalLight.transform.localEulerAngles = new Vector3(180 * worldTime.timeProgress - 180,
            defaultAngles.x, defaultAngles.z);
    }
}