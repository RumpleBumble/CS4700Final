/***************************************************************
*file: Switch.cs
*author: Emiley Thai
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 12/3/2024
*
*purpose: This manages interactions with the switch
*
****************************************************************/

using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool playerInRange;                 // If the player is close enough to interact with
    public Canvas canvas;                       // The UI the interaction prompt is written to
    private Animator anim;                      // Animation for the switch

    [SerializeField] private AudioClip lever;   // Sound effect for activating

    /**
     * Initialization of range and animator
     */
    private void Start()
    {
        playerInRange = false;
        anim = GetComponent<Animator>();
    }

    /**
     * If the player is close enough, and hits the interact key, it will toggle gravity modes
     */
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SFXManager.instance.PlaySFXClip(lever, transform, 0.5f);
            GameManager.Instance.ToggleGravityMode();
            anim.SetTrigger("Flip");
        }
    }

    /**
     * If the player is close, lets the switch be operated
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered trigger zone");
            canvas.enabled = true;
        }
    }

    /**
     * If the player is too far, it can't be operated
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited trigger zone");
            canvas.enabled = false;
        }
    }
}