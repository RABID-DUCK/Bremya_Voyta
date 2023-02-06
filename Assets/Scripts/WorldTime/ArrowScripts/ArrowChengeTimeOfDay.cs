using Photon.Pun;
using UnityEngine;

public class ArrowChengeTimeOfDay : MonoBehaviour
{
    [Header("Time setting")]
    [SerializeField] private WorldTime worldTime;

    [Header("ArrowChange settings")]
    [SerializeField] private RectTransform arrowRectTransform;

    private float angleOffsetRotationArrow;

    private float timeRotationArrow;

    private void Start()
    {
        CalcTimeRotationArrow();
        CalcAngleFromTimeProgress(timeRotationArrow);

        worldTime.OnGetTimeOfDay += ResetAngleRotate;
    }

    private void Update()
    {
        RotateArrowTimeOfDay(arrowRectTransform, angleOffsetRotationArrow);
    }

    private void ResetAngleRotate(bool timeOfDay)
    {
        if(timeOfDay && PhotonNetwork.CurrentRoom.CustomProperties["StartTime"] != null)
        {
            arrowRectTransform.Rotate(0f, 0f, 0f);
        }
        else
        {
            return;
        }
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
        if (worldTime.isStartTime && PhotonNetwork.CurrentRoom.CustomProperties["StartTime"] != null)
        {
            arrowRectTransform.Rotate(0f, 0f, -angleOffsetRotationArrow * Time.deltaTime);

        }
        else
        {
            return;
        }
    }
}