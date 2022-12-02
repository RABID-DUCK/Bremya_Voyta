using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EventController : MonoBehaviour
{
    [Tooltip("This slider is the start time of an event once every half week.\r\nOne game week equals 1820 seconds")]
    [SerializeField, Range(0, 910)] private float timeStartEvent;

    public int countEvents;

    private int randomNumberEvent;

    private void Update()
    {
        timeStartEvent += Time.deltaTime;
    }

    private void SelectRandomEvent()
    {
        randomNumberEvent = Random.Range(0, countEvents);
    }
}