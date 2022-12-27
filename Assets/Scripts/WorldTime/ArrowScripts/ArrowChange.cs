using UnityEngine;

public class ArrowChange : MonoBehaviour
{
    [SerializeField] private RectTransform arrowRectTransform;

    public float timeRotationArrow;

    public bool IsUseRotateArrowFromEventWheel;

    private float angleOffsetRotationArrow;

    private bool IsStartArrowStroke;

    private void Start()
    {
        CalcAngleOffset(timeRotationArrow);
        StartArrowStroke(IsStartArrowStroke);
    }

    private void Update()
    {
        if(IsUseRotateArrowFromEventWheel && WorldTime.CheckTimeOfDay && WorldTime.IsStartTime)
        {
            RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
        }
        else if(WorldTime.IsStartTime)
        {
            RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
        }
    }

    public void StartArrowStroke(bool IsStartArrowStroke)
    {
        IsStartArrowStroke = true;
    }

    public void RotateArrowTimeOfDay(RectTransform arrowRectTransform, float angleOffsetRotationArrow)
    {
        arrowRectTransform.Rotate(0, 0, -angleOffsetRotationArrow * Time.deltaTime);
    }

    public void CalcAngleOffset(float timeRotationArrow)
    {
        angleOffsetRotationArrow = 360 / timeRotationArrow;
    }
}