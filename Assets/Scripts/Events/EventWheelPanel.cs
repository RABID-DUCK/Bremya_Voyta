using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventWheelPanel : MonoBehaviour
{
    [SerializeField] private GameObject eventWheelPanel;
    [SerializeField] private GameObject infoEventPanel;

    [SerializeField] private Button openWheelPanel;

    [SerializeField] private Button currentEventTab;
    [SerializeField] private Button infoEventTab;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text eventPropertyText;

    [SerializeField] private ShowCanvasGroup showCanvasGroup;

    private void Start()
    {
        GameEventsStarter.OnGetEventSO += SetInfoFromEventSO;
        GameEventsStarter.OnEndEvent += SetStandartInfo;

        openWheelPanel.onClick.AddListener(OpenPanelDuringStartEvent);

        SetStandartInfo();

        ShowEventWheelPanel();
    }

    private void SetInfoFromEventSO(EventSO eventSO)
    {
        titleText.text = eventSO.eventName;

        descriptionText.text = eventSO.eventDescroption;

        eventPropertyText.text = eventSO.eventDescriptionProperties;

        OpenPanelDuringStartEvent();

        ShowEventWheelPanel();
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

    public void ShowInfoEventPanel()
    {
        currentEventTab.interactable = true;
        infoEventTab.interactable = false;

        infoEventPanel.SetActive(true);
        eventWheelPanel.SetActive(false);
    }

    public void ShowEventWheelPanel()
    {
        infoEventTab.interactable = true;
        currentEventTab.interactable = false;

        infoEventPanel.SetActive(false);
        eventWheelPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        GameEventsStarter.OnGetEventSO -= SetInfoFromEventSO;
        GameEventsStarter.OnEndEvent -= SetStandartInfo;
    }
}