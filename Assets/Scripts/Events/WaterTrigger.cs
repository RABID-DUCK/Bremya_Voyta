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
            CharacterController _controller = other.GetComponent<CharacterController>();

            Transform posHouse = houses.FirstOrDefault(h => other.gameObject.GetPhotonView().Owner.NickName == h.playerNick).doorway;

            _controller.enabled = false;
            other.transform.position = other.transform.TransformVector(posHouse.position);
            other.transform.GetChild(0).rotation = posHouse.transform.rotation;
            _controller.enabled = true;
        }
    }
}
