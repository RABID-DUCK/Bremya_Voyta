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

    private void Awake()
    {
        
    }

    private void Start()
    {
/*        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(player, spawnPosition);
            newPlayerItem.Set(OpenOrCloseCharacters, this);
            newPlayerItem.SetPlayerInfo(_player.Value);

            playerItemsList.Add(newPlayerItem);
        }*/
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
