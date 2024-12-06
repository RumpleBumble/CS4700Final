/***************************************************************
*file: PlayerController.cs
*author: Richard Corvera
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 12/3/2024
*
*purpose: This manages the player's movement
*
****************************************************************/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 15f;            // Movement speed
    private bool isGrounded;                    // Is the player on the ground?
    private Rigidbody rb;                       // Player's rigid body
    public Transform cam;                       // The camera that will follow the player

    // Gravity Mode variables
    private const float LOW_GRAV_JUMP = 10f;    // Jump force under low gravity
    private const float HIGH_GRAV_JUMP = 7f;    // Jump force under high gravity

    // Sound Effect Fields
    [SerializeField] private AudioClip walkSound;   // Walking sound effect
    private bool isWalking;                         // Plays sound while walking 

    /**
     * Initializes the rigid body and player states
     */
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        isWalking = false;
    }

    /**
     * Handles movement and jumping on frames
     */
    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    /**
     * Reads input to move the player
     */
    void MovePlayer()
    {   // Vertical/Horizontal inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // We're going to make the movement based on the perspective of the camera
        Vector3 cameraForward = cam.forward;
        Vector3 cameraRight = cam.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        
        // Converts the movement axis to be relative to the camera
        Vector3 relativeForward = moveVertical * cameraForward;
        Vector3 relativeRight = moveHorizontal * cameraRight;

        // Now we move the character
        Vector3 movementDirection = relativeForward + relativeRight;
        Vector3 movement = new Vector3(movementDirection.x, 0.0f, movementDirection.z);
        rb.MovePosition(transform.position + movement * SPEED * Time.deltaTime);
        
        // If we moved at all or not
        if (movementDirection != Vector3.zero)
        {   // Rotate the player's body to direction of trabel
            Vector3 rotation = Quaternion.LookRotation(movementDirection).eulerAngles - new Vector3(0, 90, 0);
            rb.MoveRotation(Quaternion.Euler(rotation));

            if (!isWalking) // Players the walking sound
            {
                SFXManager.instance.PlayLoopingSFX("Walking", walkSound, transform, 0.5f);
                isWalking = true;
            }
        }
        else
        {   // Stops the walking sound
            if (isWalking)
            {
                SFXManager.instance.StopLoopingSFX("Walking");
                isWalking = false;
            }
        }
    }

    /**
     * Handles jumping
     */
    void Jump()
    {
        float currentJumpForce = GameManager.Instance.isLowGravity ? LOW_GRAV_JUMP : HIGH_GRAV_JUMP;
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    /**
     * Updates if the player is grounded or not
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
