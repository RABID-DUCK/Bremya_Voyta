using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float vertical, horizontal;
    private float mouseX, mouseY;
    private float minAngle, maxAngle;

    [Header("Скорость перемещения персонажа")]
    [SerializeField] private float speed = 7f;

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
        MouseTarget();
    }
    void PlayerMov()
    {
        horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(new Vector3(horizontal, 0, vertical));
    }
    void MouseTarget()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.fixedDeltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.fixedDeltaTime;

        //camera.transform.rotation=Quaternion.Euler(-mouseY, mouseX, 0f);
        //player.transform.rotation = Quaternion.Euler(0f, mouseX, 0f);

        player.transform.Rotate(new Vector3(0, mouseX, 0));
        
        camera.transform.Rotate(new Vector3(-mouseY, 0, 0));
    }
}
