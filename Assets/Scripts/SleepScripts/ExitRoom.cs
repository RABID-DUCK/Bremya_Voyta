using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitRoom : MonoBehaviour
{
    Outline outline;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button declineBtn;
    [SerializeField] private GameObject panelExit;

    void Start()
    {
        outline = transform.gameObject.GetComponent<Outline>();
    }
    
    private void OnMouseOver()
    {
        if (outline != null) 
        {
            print("ne null;");
        }
        else
        {
            print("null");
        }
        outline.enabled = true;

        if (Input.GetMouseButtonDown(0))
        {
            panelExit.SetActive(true);
        }
    }
    
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
    
    //private void OnMouseDown()
    //{
    //    panelExit.SetActive(true);
    //}

    public void SelectedConfirm()
    {
        SceneManager.LoadScene("CityScene");
    }

    public void SelectedDecline()
    {
        panelExit.SetActive(false);
    }
}
