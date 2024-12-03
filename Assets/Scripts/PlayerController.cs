using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
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
        if (Input.GetKeyDown(KeyCode.G)) // Press 'G' to toggle gravity mode
        {
            ToggleGravityMode();
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
        if (movementDirection != new Vector3(0, 0, 0))
        {
            Vector3 rotation = Quaternion.LookRotation(movementDirection).eulerAngles - new Vector3(0, 90, 0);
            rb.MoveRotation(Quaternion.Euler(rotation));
        }
            
    }

    void Jump()
    {
        float currentJumpForce = isLowGravity ? lowGravityJumpForce : highGravityJumpForce;
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    public void ToggleGravityMode()
    {
        isLowGravity = !isLowGravity;
        Debug.Log("Gravity Mode: " + (isLowGravity ? "Low Gravity" : "High Gravity"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
