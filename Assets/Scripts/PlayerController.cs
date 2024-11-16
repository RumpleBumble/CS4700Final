using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody rb;

    // Gravity Mode variables
    private bool isLowGravity = true;
    public float lowGravityJumpForce = 10f;
    public float highGravityJumpForce = 7f;

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

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    void Jump()
    {
        float currentJumpForce = isLowGravity ? lowGravityJumpForce : highGravityJumpForce;
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void ToggleGravityMode()
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
