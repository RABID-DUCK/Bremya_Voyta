using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float vertical, horizontal;
    Bed bed;

    [Header("Скорость перемещения персонажа")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float turningSpeed = 7f;

    [SerializeField] private PhotonView photonView;

    [SerializeField] private float sensitivityMouse;

    [SerializeField] private Transform playerBody;

    private void Awake()
    {
        if (!photonView)
        {
            photonView = GetComponent<PhotonView>();
        }
        if (!photonView.IsMine)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
            vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            playerBody.transform.Rotate(0, horizontal, 0);
            transform.Translate(0, 0, vertical);
        }
    }
    public void WindowSleep()
    {

    }
}
