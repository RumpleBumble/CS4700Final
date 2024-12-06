/***************************************************************
*file: WeightButton.cs
*author: Emiley Thai
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 12/3/2024
*
*purpose: This manages the weighed button's interactions
*
****************************************************************/

using UnityEngine;

public class WeightButton : MonoBehaviour
{
    public enum activationColor { red, blue, green }  // Possible colors for the button
    
    private static readonly int COLOR1 = Shader.PropertyToID("_Color"); // Used color in the button

    public activationColor color;         // Color for this specific button
    public WalledDoor[] connectedDoors;   // Doors that this button is connected to
    private bool isPressed;               // If the button is pressed
    private Animator buttonAnimator;      // Button animator  
    public GameObject fillin;             // Part of the button to color in

    /**
     * Colors in the button upon level entry and gets the animator
     */
    void Start()
    {
        setColor(color, fillin);
        buttonAnimator = GetComponent<Animator>();
    }

    /**
     * Checks to see if the button should be released
     */
    private void Update()
    {
        if (isPressed && GameManager.Instance.isLowGravity)
        {
            ReleaseButton();
        }
    }

    /**
     * Activates if a box is touching and it's high gravity
     */
    private void OnTriggerStay(Collider other)
    {
        if (!isPressed && other.attachedRigidbody != null && other.CompareTag("Box"))
        {
            if (!GameManager.Instance.isLowGravity)
                PressButton();
        }
    }

    /**
     * Releases when there's nothing on top
     */
    private void OnTriggerExit(Collider other)
    {
        if (isPressed)
        {
            ReleaseButton();
        }
    }

    /**
     * Activates all doors connected to the button
     */
    void PressButton()
    {
        isPressed = true;
        buttonAnimator?.SetTrigger("Press");
        foreach (WalledDoor door in connectedDoors)
        {
            door.OpenDoor();
            door.GetComponent<BoxCollider>().enabled = false;
        }
    }

    /**
     * Deactivates all doors this button is connected to
     */
    void ReleaseButton()
    {
        isPressed = false;
        buttonAnimator?.SetTrigger("Release");
        foreach (WalledDoor door in connectedDoors)
        {
            door.CloseDoor();
            door.GetComponent<BoxCollider>().enabled = true;
        }
    }

    /**
     * Colors in part of the button to the activation color
     */
    public static void setColor(activationColor color, GameObject fillin)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        Color startColor = color switch
        {
            activationColor.red => Color.red,
            activationColor.blue => Color.blue,
            activationColor.green => Color.green,
            _ => Color.white
        };
        props.SetColor(COLOR1, startColor);
        fillin.GetComponent<Renderer>().SetPropertyBlock(props);
    }
}

