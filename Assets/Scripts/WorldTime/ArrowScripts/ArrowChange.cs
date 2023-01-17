using UnityEngine;

public class ArrowChange : MonoBehaviour
{
    [Header("Time setting")]
    [SerializeField] private WorldTime worldTime;

    [SerializeField] private RectTransform arrowRectTransform;

    [SerializeField] private bool IsUseRotateArrowFromEventWheel;

    private float angleOffsetRotationArrow;

    private float timeRotationArrow;

    private void Start()
    {
        CalcTimeRotationArrow(timeRotationArrow);

        CalcAngleOffset(timeRotationArrow);
    }

    private void Update()
    {
        if (IsUseRotateArrowFromEventWheel && worldTime.CheckTimeOfDay && worldTime.IsStartTime)
        {
            RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
        }
        else if (worldTime.IsStartTime)
        {
            RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
        }
    }

    public void RotateArrowTimeOfDay(RectTransform arrowRectTransform, float angleOffsetRotationArrow)
    {
        arrowRectTransform.Rotate(0f, 0f, -angleOffsetRotationArrow * Time.deltaTime);
    }

    public void CalcAngleOffset(float timeRotationArrow)
    {
        if (IsUseRotateArrowFromEventWheel)
        {
            angleOffsetRotationArrow = 360 / timeRotationArrow;
        }
    }

    public void CalcTimeRotationArrow(float timeRotationArrow)
    {
        if (IsUseRotateArrowFromEventWheel)
        {
            timeRotationArrow = (worldTime.dayTimeInSeconds + worldTime.nightTimeInSeconds) * 6;

            print($"Секунд днем {worldTime.dayTimeInSeconds}, секунд ночью {worldTime.nightTimeInSeconds}");

            print(arrowRectTransform);
        }
        else
        {
            timeRotationArrow = worldTime.dayTimeInSeconds + worldTime.nightTimeInSeconds;
        }
    }
}