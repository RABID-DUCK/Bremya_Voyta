using JetBrains.Annotations;
using UnityEngine;

public class EventWheelController : MonoBehaviour
{
    [Tooltip("This slider is the start time of an event once every half week.\r\nOne game week equals 1820 seconds")]
    [SerializeField, Range(0, 910)] private float timeStartEvent;

    private void Update()
    {
        timeStartEvent += Time.deltaTime;
    }

    public void SelectRandomEvents()
    {
        int randomNum = Random.Range(1, 6);

        StartRandomEvent(randomNum);
    }

    public void StartRandomEvent(int currenEvent)
    {
        //if (WorldTime.timeDayInSeconds == timeStartEvent)
        //{
        //    timeStartEvent = 0;

        //    Дописать логику
        //}
    }
}