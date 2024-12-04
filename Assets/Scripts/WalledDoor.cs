using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalledDoor : MonoBehaviour
{
    public WeightButton.activationColor color;
    public GameObject fillin;
    private bool isClosed = true;
    public Animator doorAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        WeightButton.setColor(color, fillin);
        doorAnimator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (isClosed)
        {
            doorAnimator.SetTrigger("Open");
            isClosed = false;
        }
    }

    public void CloseDoor()
    {
        if (!isClosed)
        {
            doorAnimator.SetTrigger("Close");
            isClosed = true;
        }
    }
}
