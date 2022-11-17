using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float vertical, horizontal;
    //private float mouseX, mouseY;
    //private float minAngle, maxAngle;

    [Header("Скорость перемещения персонажа")]
    [SerializeField] private float speed = 7f;
    [SerializeField] private float turningSpeed = 7f;

    [SerializeField] private PhotonView view;

    [SerializeField] private float sensitivityMouse;

    [SerializeField] private GameObject camera;
    //[SerializeField] private Transform cameraTarget;

    [SerializeField] private PlayerController scriptPlayerController;
    [SerializeField] private Transform player;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            camera.SetActive(false);
            scriptPlayerController.enabled = false;
        }
    }
    private void Update()
    {
        PlayerMov();
    }
    void PlayerMov()
    {
        horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        player.transform.Rotate(0, horizontal, 0);
        transform.Translate(0, 0, vertical);

    }
}
