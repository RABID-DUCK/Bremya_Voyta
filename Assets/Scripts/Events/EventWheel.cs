using UnityEngine;

[CreateAssetMenu(fileName = "EventWheel", menuName = "EventWheel")]
public class EventWheel : ScriptableObject
{
    [Tooltip("Name of the event")]
    public string eventName;

    [Tooltip("Event description"), Multiline(15)]
    public string eventDiscription;
}
