using UnityEngine;

public class ArrowChangeTimeOfDay : MonoBehaviour
{
    [Header("Time setting")]
    [SerializeField] private WorldTime worldTime;

    [Header("ArrowChange settings")]
    [SerializeField] private RectTransform arrowRectTransform;

    [SerializeField] private SleepPresenter sleepPresenter;

    private float angleOffsetRotationArrow;

    private float timeRotationArrow;

    private void Start()
    {
        CalcTimeRotationArrow();
        CalcAngleFromTimeProgress(timeRotationArrow);

        sleepPresenter.OnSkipArrow += ResetAngleRotate;
    }

    private void Update()
    {
        RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
    }

    private void ResetAngleRotate()
    {
        arrowRectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private float CalcTimeRotationArrow()
    {
        return timeRotationArrow = worldTime.dayTimeInSeconds + worldTime.nightTimeInSeconds;
    }

    private void CalcAngleFromTimeProgress(float timeRotationArrow)
    {
        angleOffsetRotationArrow = 360f / timeRotationArrow;
    }

    private void RotateArrowTimeOfDay(RectTransform arrowRectTransform, float angleOffsetRotationArrow)
    {
        if (worldTime.isStartTime)
        {
            arrowRectTransform.Rotate(0f, 0f, -angleOffsetRotationArrow * Time.deltaTime);
        }
    }
}