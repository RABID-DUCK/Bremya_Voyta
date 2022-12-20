using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;

    private float vertical, horizontal;
    Bed bed;
    Vector3 test;
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
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
    }
    //private void MoveUnit()
    //{
    //    switch (controlType)
    //    {
    //        case ControlTypes.Keybord:
    //            moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed_X, 0f, Input.GetAxis("Vertical") * speed_Z);
    //            break;
    //        case ControlTypes.VirtualJoystick:
    //            moveDirection = new Vector3(moveJoystick.AxisX * speed_X, 0f, moveJoystick.AxisY * speed_Z);
    //            break;
    //    }
    //    moveDirection = Camera.mainCamera.transform.TransformDirection(moveDirection);
    //    controller.SimpleMove(moveDirection);

    //    Vector3 lookDirection = moveDirection + transform.position;

    //    transform.LookAt(new Vector3(lookDirection.x, transform.position.y, lookDirection.z));
    //}

    private void PlayerMove()
    {
        /*                if (PhotonNetwork.LocalPlayer.IsLocal)
                        {


                        }
        */
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            GetComponentInChildren<Animator>().Play("female_walk_anim");
        }
        else
        {
            GetComponentInChildren<Animator>().Play("female_idle_anim");
        }

        var h = -Input.GetAxis("Horizontal"); // AD
        var v = -Input.GetAxis("Vertical"); // WS


        // Move player
        var right = transform.right * h;
        var forward = transform.forward * v;
        var moveDir = right + forward;
        cc.Move(moveDir * moveSpeed * Time.deltaTime);

        // Rotate graphics
        if (moveDir.normalized.magnitude < 0.001f) return;
        var targetRot = Quaternion.LookRotation(moveDir, Vector3.up);
        playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRot, turningSpeed * Time.deltaTime); // Smooth

    
    }
    public void WindowSleep()
    {

    }
}
