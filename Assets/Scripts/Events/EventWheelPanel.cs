using TMPro;
using UnityEngine;

public class EventWheelPanel : MonoBehaviour
{
    [SerializeField] private GameObject eventWheelPanel;
    [SerializeField] private ShowCanvasGroup showCanvasGroup;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text eventPropertyText;

    private void Start()
    {
        EventController.OnGetEventSO += SetInfoFromEventSO;
        EventController.OnEndEvent += SetStandartInfo;

        SetStandartInfo();
    }

    private void SetInfoFromEventSO(EventSO eventSO)
    {
        titleText.text = eventSO.eventName;

        descriptionText.text = eventSO.eventDescroption;

        eventPropertyText.text = eventSO.eventDescriptionProperties;

        OpenPanelDuringStartEvent();
    }

    private void SetStandartInfo()
    {
        titleText.text = "Пока никаких событий нет";

        descriptionText.text = "Описание события";

        eventPropertyText.text = "Описание свойств события";
    }

    public void OpenPanelDuringStartEvent()
    {
        showCanvasGroup.Show();
    }
}