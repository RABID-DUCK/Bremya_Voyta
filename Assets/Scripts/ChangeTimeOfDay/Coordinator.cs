using UnityEngine;

public class Coordinator : MonoBehaviour
{
    [Header("CoordinatorEvents settings")]

    [SerializeField] private WorldTime worldTime;
    [SerializeField] private ChangeDayAndNight changeDayAndNight;

    private Gradient standartDayGradient;
    private Gradient standartNightGradient;

    [Header("Day and night settings when it rains")]
    [Tooltip("Gradient of the day")]
    public Gradient directionalLightGradient;
    [Tooltip("Gradient of the night")]
    public Gradient ambientLightGradient;

    private void Start()
    {
        standartDayGradient = changeDayAndNight.directionalLightGradient;

        standartNightGradient = changeDayAndNight.ambientLightGradient;
    }

    
}