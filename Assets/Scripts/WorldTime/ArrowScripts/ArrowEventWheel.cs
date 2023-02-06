using Photon.Pun;
using UnityEngine;

public class ArrowEventWheel : MonoBehaviour
{
    [Header("Time setting")]
    [SerializeField] private WorldTime worldTime;

    [Header("ArrowChange settings")]
    [SerializeField] private RectTransform arrowRectTransform;

    private float timeRotationArrow;

    private float angleOffsetRotationArrow;

    private void Start()
    {
        CalcTimeRotation();

        CalcAngleOffset(timeRotationArrow);
    }

    private void Update()
    {
        RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
    }

    private float CalcTimeRotation()
    {
        return timeRotationArrow = worldTime.dayTimeInSeconds * 6f;
    }

    private void CalcAngleOffset(float timeRotationArrow)
    {
        angleOffsetRotationArrow = 360f / timeRotationArrow;
    }

    private void RotateArrowTimeOfDay(RectTransform arrowRectTransform, float angleOffsetRotationArrow)
    {
        if (worldTime.isCheckTimeOfDay && worldTime.isStartTime && PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
        {
            arrowRectTransform.Rotate(0f, 0f, -angleOffsetRotationArrow * Time.deltaTime);
        }
        else
        {
            return;
        }
    }
}