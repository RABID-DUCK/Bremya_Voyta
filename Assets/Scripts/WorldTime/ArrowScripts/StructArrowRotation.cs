using UnityEngine;

public class StructArrowRotation : MonoBehaviour
{
    public virtual void StartArrowStroke(bool IsStartArrowStroke)
    {
        IsStartArrowStroke = true;
    }

    public virtual void RotateArrowTimeOfDay(RectTransform arrowRectTransform, float angleOffsetRotationArrow)
    {
        arrowRectTransform.Rotate(0, 0, -angleOffsetRotationArrow * Time.deltaTime);
    }

    public virtual void CalcAngleOffset(float angleOffsetRotationArrow, float timeRotationArrow)
    {
        angleOffsetRotationArrow = 360 / timeRotationArrow;
    }
}