using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "EventSO")]
public class EventSO : ScriptableObject
{
    [Header("Event data settings")]

    [Tooltip("The name of the event that will appear in the event panel")]
    public string eventName;

    [TextArea(5, 10), Tooltip("Description of the event that will appear in the event panel")]
    public string eventDescroption;

    [TextArea(5, 10), Tooltip("Description of the event property that will appear in the event panel")]
    public string eventDescriptionProperties;
}
