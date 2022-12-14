using UnityEngine;

public class ChangeDayAndNight : MonoBehaviour
{
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
        if (WorldTime.CheckTimeOfDay)
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
        directionalLight.color = dayGradient.Evaluate(WorldTime.timeProgress);

        RenderSettings.ambientLight = daySkyGradient.Evaluate(WorldTime.timeProgress);

        directionalLight.transform.localEulerAngles = new Vector3(180 * WorldTime.timeProgress,
            defaultAngles.x, defaultAngles.z);
    }

    private void ChangingGradientColorNught()
    {
        directionalLight.color = nightGradient.Evaluate(WorldTime.timeProgress);

        RenderSettings.ambientLight = nightSkyGradient.Evaluate(WorldTime.timeProgress);

        directionalLight.transform.localEulerAngles = new Vector3(180 * WorldTime.timeProgress - 180,
            defaultAngles.x, defaultAngles.z);
    }
}