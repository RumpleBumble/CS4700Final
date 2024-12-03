using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 35f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody rb;
    public Transform cam;

    // Gravity Mode variables
    private bool isLowGravity = true;
    public float lowGravityJumpForce = 10f;
    public float highGravityJumpForce = 7f;

    // Sound Effect Fields
    [SerializeField] private AudioClip walkSound;
    private bool isWalking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = cam.forward;
        Vector3 cameraRight = cam.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 relativeForward = moveVertical * cameraForward;
        Vector3 relativeRight = moveHorizontal * cameraRight;

        Vector3 movementDirection = relativeForward + relativeRight;
        
        Vector3 movement = new Vector3(movementDirection.x, 0.0f, movementDirection.z);
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        
        if (movementDirection != Vector3.zero)
        {
            Vector3 rotation = Quaternion.LookRotation(movementDirection).eulerAngles - new Vector3(0, 90, 0);
            rb.MoveRotation(Quaternion.Euler(rotation));

            if (!isWalking)
            {
                SFXManager.instance.PlayLoopingSFX("Walking", walkSound, transform, 0.5f);
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                SFXManager.instance.StopLoopingSFX("Walking");
                isWalking = false;
            }
        }
    }

    void Jump()
    {
        float currentJumpForce = GameManager.Instance.isLowGravity ? lowGravityJumpForce : highGravityJumpForce;
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
