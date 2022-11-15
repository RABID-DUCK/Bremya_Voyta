using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickEventer : MonoBehaviour
{
    [SerializeField] GameObject popup;


     void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            RaycastHit _hit;
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
            {
                switch (_hit.transform.tag)
                {
                    case "House":
                        {
                            Debug.Log("dsad");
                            popup.SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void ChooseBtnYes()
    {
        SceneManager.LoadScene("InHome");
    }

    public void ChooseBtnNo()
    {
        popup.SetActive(false);
    }
}
