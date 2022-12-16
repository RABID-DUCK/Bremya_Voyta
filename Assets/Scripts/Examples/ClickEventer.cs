using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickEventer : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject popup;
    [SerializeField] GameObject player;
    [SerializeField] public GameObject[] Spawns;
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if(_photonView.IsMine)
        {
            CreateController();
        }

        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            Vector3 randomPositions = Spawns[Random.Range(0, Spawns.Length)].transform.localPosition;
            PhotonNetwork.Instantiate(player.name, randomPositions, Quaternion.identity);
            print(_player.Value.NickName);
        }
    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Hunter_1"), Vector3.zero, Quaternion.identity);
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
