using UnityEngine;

public class Bed : MonoBehaviour
{
    Outline outline;
    SleepPanel sleepPanel;

    [SerializeField] private LayerMask sleepLayerMask;
    [SerializeField] private GameObject sleepWindow;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject playerSleep;

    [SerializeField] private bool dayTime;
    void Start()
    {
        outline = transform.gameObject.GetComponent<Outline>();
    }
    private void OnMouseOver()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, sleepLayerMask))
            {
                StateSleep();
            }
        }
    }
    //private void OnMouseDown()
    //{
    //    StateSleep();
    //}
    public void StateSleep()
    {
        //sleepWindow.SetActive(true);

        if (!WorldTime.CheckTimeOfDay)
        {
            print("night");
            sleepWindow.SetActive(true);
            sleepPanel.ShowSleepPanel();
        }
        else
        {
            print("day");
            print(WorldTime.timeProgress);
            playerSleep.SetActive(false);
            Player.SetActive(true);
        }
    }
}
