/***************************************************************
*file: WalledDoor.cs
*author: Andy Nguyen
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 12/3/2024
*
*purpose: This manages the door opening and closing. This should
 *        be called from the Weight Buttons
*
****************************************************************/

using UnityEngine;

public class WalledDoor : MonoBehaviour
{
    public WeightButton.activationColor color;  // Color of the door light
    public GameObject fillin;                   // Part of door to be colored
    private bool isClosed;                      // Is the door currently close?
    public Animator doorAnimator;               // The door's animation controller
    
    // Colors the door light and starts it closed
    void Start()
    {
        isClosed = true;
        WeightButton.setColor(color, fillin);
        doorAnimator = GetComponent<Animator>();
    }

    // Opens the door
    public void OpenDoor()
    {
        if (isClosed)
        {
            doorAnimator.SetTrigger("Open");
            isClosed = false;
        }
    }

    // Closes the door
    public void CloseDoor()
    {
        if (!isClosed)
        {
            doorAnimator.SetTrigger("Close");
            isClosed = true;
        }
    }
}
