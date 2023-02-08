using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterTrigger : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<House> houses;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetPhotonView().IsMine)
        {
            Character character = other.GetComponent<Character>();

            Transform posHouse = houses.FirstOrDefault(h => other.gameObject.GetPhotonView().Owner.NickName == h.playerNick).doorway;

            character.Teleport(posHouse.position, posHouse.transform.rotation);
        }
    }
}
