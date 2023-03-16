using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    CharacterController cc;

    [SerializeField] private float gravity = 9.8f;
    [Header("�������� ����������� ���������")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float turningSpeed = 7f;
    private float vSpeed = 0;

    [SerializeField] private Transform playerBody;

    [SerializeField] private AudioSource playerWalckSound;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
        cc = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        var h = Input.GetAxis("Horizontal"); // AD
        var v = Input.GetAxis("Vertical"); // WS

        if (h != 0 || v != 0)
        {
            if (!playerWalckSound.isPlaying)
            {
                playerWalckSound.Play();
            }
        }
        else
        {
            playerWalckSound.Stop();
        }

        // Move player
        var right = transform.right * h;
        var forward = transform.forward * v;
        vSpeed -= (cc.isGrounded ? 0 : gravity) * Time.deltaTime;
        var up = transform.up * vSpeed;
        var moveDir = right + forward;
        cc.Move((moveDir.normalized + up) * moveSpeed * Time.deltaTime);

        // Rotate graphics
        if (moveDir.normalized.magnitude > 0.001f)
        {
            var targetRot = Quaternion.LookRotation(moveDir, Vector3.up);
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRot, turningSpeed * Time.deltaTime); // Smooth
        }

        animator.SetBool("walk", (h != 0 || v != 0 || moveDir.normalized.magnitude > 0.001f));
    }
    public void WindowSleep()
    {

    }
}
