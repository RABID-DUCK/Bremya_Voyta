using UnityEngine;

[ExecuteInEditMode]
public class ChangeDayAndNight : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;

    [Tooltip("Directional light")]
    [SerializeField] private Light directionalLight;

    [Header("Time of day color settings")]
    [Tooltip("Gradient of the day")]
    public Gradient directionalLightGradient;
    [Tooltip("Gradient of the night")]
    public Gradient ambientLightGradient;

    [Space, Tooltip("Can I change the color of the directional light or not?")]
    public bool isChangeColorLight = true;

    private Vector3 defaultAngles;

    private void Start()
    {
        defaultAngles = directionalLight.transform.localEulerAngles;
    }

    private void Update()
    {
        if (isChangeColorLight)
        {
            ChangeGradientColorWithDayTime();
        }
    }

    private void ChangeGradientColorWithDayTime()
    {
        directionalLight.color = directionalLightGradient.Evaluate(worldTime.timeProgress);

        RenderSettings.ambientLight = ambientLightGradient.Evaluate(worldTime.timeProgress);

        directionalLight.transform.localEulerAngles = new Vector3(360f * worldTime.timeProgress - 90,
            defaultAngles.x, defaultAngles.z);
    }
}