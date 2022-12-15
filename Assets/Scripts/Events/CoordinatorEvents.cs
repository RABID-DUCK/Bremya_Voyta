using UnityEngine;

public class CoordinatorEvents : MonoBehaviour
{
    [Header("CoordinatorEvents settings")]

    [SerializeField] private WorldTime worldTime;
    [SerializeField] private ChangeDayAndNight changeDayAndNight;

    private Gradient standartDayGradient;
    private Gradient standartSkyColor;

    [Header("Gradient of day and night light during bad weather")]
    [Tooltip("Gradient of the day")]
    public Gradient dayLightGradient;
    [Tooltip("Gradient of the night")]
    public Gradient nightSkyColor;

    private void Start()
    {
        standartDayGradient = changeDayAndNight.dayGradient;

        standartSkyColor = changeDayAndNight.daySkyGradient;

        EventController.GetNegativeWeather += CheckNegativeWeather;
    }

    public void CheckNegativeWeather(bool isNegativeWeather)
    {
        if (isNegativeWeather)
        {
            changeDayAndNight.dayGradient = dayLightGradient;

            changeDayAndNight.daySkyGradient = nightSkyColor;
        }
        else
        {
            changeDayAndNight.dayGradient = standartDayGradient;

            changeDayAndNight.daySkyGradient = standartSkyColor;
        }
    }

    //[ContextMenu("Added standart gradient settings")]
    //public void GetStandartGradientSettings()
    //{
    //    dayLightGradient = standartDayGradient;

    //    nightSkyColor = standartSkyColor;
    //}
}