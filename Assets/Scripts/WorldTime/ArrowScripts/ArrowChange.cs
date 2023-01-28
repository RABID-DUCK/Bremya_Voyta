using UnityEngine;

public class ArrowChange : MonoBehaviour
{
    [Header("Time setting")]
    [SerializeField] private WorldTime worldTime;

    [Header("ArrowChange settings")]
    [Space, SerializeField] private RectTransform arrowRectTransform;

    [SerializeField] private float timeRotationArrow;

    [Space, SerializeField] private bool IsUseRotateArrowFromEventWheel;

    private float angleOffsetRotationArrow;

    private void Start()
    {
        CalcAngleOffset(timeRotationArrow);
    }

    private void Update()
    {
        if (IsUseRotateArrowFromEventWheel && worldTime.isCheckTimeOfDay && worldTime.isStartTime)
        {
            RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
        }
        else if (worldTime.isStartTime && !IsUseRotateArrowFromEventWheel)
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
        angleOffsetRotationArrow = 360 / timeRotationArrow;
    }
}