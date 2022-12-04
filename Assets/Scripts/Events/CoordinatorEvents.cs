using UnityEngine;

public class CoordinatorEvents : MonoBehaviour
{
    [Header("CoordinatorEvents settings")]

    [SerializeField] private WorldTime worldTime;
    [SerializeField] private ChangeDayAndNight changeDayAndNight;

    private Gradient standartDayGradient;
    private Gradient standartNightGradient;

    [Header("Gradient of day and night light during bad weather")]
    [Tooltip("Gradient of the day")]
    public Gradient dayLightGradient;
    [Tooltip("Gradient of the night")]
    public Gradient nightLightGradient;

    private void Start()
    {
        standartDayGradient = changeDayAndNight.directionalLightGradient;

        standartNightGradient = changeDayAndNight.ambientLightGradient;

        EventController.GetNegativeWeather += CheckNegativeWeather;
    }

    public void CheckNegativeWeather(bool isNegativeWeather)
    {
        if (isNegativeWeather)
        {
            changeDayAndNight.directionalLightGradient = dayLightGradient;

            changeDayAndNight.ambientLightGradient = nightLightGradient;
        }
        else
        {
            changeDayAndNight.directionalLightGradient = standartDayGradient;

            changeDayAndNight.ambientLightGradient = standartNightGradient;
        }
    }

    //[ContextMenu("Added standart gradient settings")]
    //public void GetStandartGradientSettings()
    //{
    //    dayLightGradient = standartDayGradient;

    //    nightLightGradient = standartNightGradient;
    //}
}