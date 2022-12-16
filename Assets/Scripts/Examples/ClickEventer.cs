using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickEventer : MonoBehaviour
{
    [SerializeField] GameObject popup;
    [SerializeField] GameObject player;
    [SerializeField] public GameObject[] Spawns;

    private void Awake()
    {
        Vector3 randomPositions = Spawns[Random.Range(0, Spawns.Length)].transform.localPosition;
        PhotonNetwork.Instantiate(player.name, randomPositions, Quaternion.identity);
    }

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
        SceneManager.LoadScene(3);
    }

    public void ChooseBtnNo()
    {
        popup.SetActive(false);
    }
    

}
