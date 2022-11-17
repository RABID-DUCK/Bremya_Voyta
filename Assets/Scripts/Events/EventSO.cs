using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSO : MonoBehaviour
{
    [Header("Event data settings")]

    [Tooltip("The name of the event that will appear in the event panel")]
    public string eventName;

    [Multiline(5), Tooltip("Description of the event that will appear in the event panel")]
    public string eventDescroption;

    [Multiline(5), Tooltip("Description of the event property that will appear in the event panel")]
    public string eventDescriptionProperties;
}
