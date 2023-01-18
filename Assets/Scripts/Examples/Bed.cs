using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;

    Outline outline;
    SleepPanel sleepPanel;

    [SerializeField] private LayerMask sleepLayerMask;
    [SerializeField] private GameObject sleepWindow;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSleep;

    [SerializeField] private WarningSleepPanel warningSleepPanel;
    private bool IsSleep { get; set; }

    private void OnEnable()
    {
        worldTime = FindObjectOfType<WorldTime>();
    }


    private void Start()
    {
        outline = transform.gameObject.GetComponent<Outline>();

        sleepPanel.OnSleep += Sleep;

        //warningSleepPanel.OnDontSleep += ForcedSleep;
    }

    private void OnMouseOver()
    {
        outline.enabled = true;

        if (Input.GetMouseButtonDown(0))
        {
            StateSleep();
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    private void Update()
    {
        if (worldTime.CheckTimeOfDay)
        {
            Instantiate(player, new Vector3(0.2f, 0.08f, 0.7f), Quaternion.LookRotation(new Vector3(0, 0, 0)));

            IsSleep = false;
        }
    }

    private void StateSleep()
    {
        if (!worldTime.CheckTimeOfDay)
        {
            print("night");
            sleepWindow.SetActive(true);
        }
        else
        {
            print("day");
            print(worldTime.CheckTimeOfDay);
        }
    }

    private void Sleep()
    {
        Instantiate(player, new Vector3(-0.984f, 0.4f, -0.261f), Quaternion.LookRotation(new Vector3(0, 90f, 0)));

        IsSleep = true;
    }
}

    
    
    
    
    //
    //void Start()
    //{
    //    outline = transform.gameObject.GetComponent<Outline>();
    //}
    //
    //private void OnMouseOver()
    //{
    //    outline.enabled = true;
    //}
    //
    //private void OnMouseExit()
    //{
    //    outline.enabled = false;
    //}
    //
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit, sleepLayerMask))
    //        {
    //            StateSleep();
    //        }
    //    }
    //}
    ////private void OnMouseDown()
    ////{
    ////    StateSleep();
    ////}
    //public void StateSleep()
    //{
    //    //sleepWindow.SetActive(true);
    //
    //    if (!worldTime.CheckTimeOfDay)
    //    {
    //        print("night");
    //        sleepWindow.SetActive(true);
    //        sleepPanel.ShowSleepPanel();
    //    }
    //    else
    //    {
    //        print("day");
    //        print(worldTime.timeProgress);
    //        playerSleep.SetActive(false);
    //        Player.SetActive(true);
    //    }
    //}

