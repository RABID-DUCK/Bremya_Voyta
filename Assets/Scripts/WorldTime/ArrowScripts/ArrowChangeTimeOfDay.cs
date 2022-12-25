using Ekonomika.Dialog;
using UnityEngine;

public class ArrowChangeTimeOfDay : StructArrowRotation
{
    [SerializeField] private RectTransform arrowRectTransform;

    public float timeRotationArrow;

    private float angleOffsetRotationArrow;

    private bool IsStartArrowStroke;

    private void Start()
    {
        CalcAngleOffset(angleOffsetRotationArrow, timeRotationArrow);

        DialogPresenter.OnDialogEnd += StarterArrowStroke;
    }

    private void Update()
    {
        RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
    }

    public void StarterArrowStroke()
    {
        StartArrowStroke(IsStartArrowStroke);
    }
}