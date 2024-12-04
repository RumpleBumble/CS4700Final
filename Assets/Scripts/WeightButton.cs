// WeightButton.cs

using System;
using UnityEngine;
using UnityEngine.Events;

public class WeightButton : MonoBehaviour
{
    public enum activationColor { red, blue, green }
    public activationColor color;
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    public WalledDoor[] connectedDoors;
    private bool isPressed = false;
    private float weightThreshold = 2f;
    private Animator buttonAnimator;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public GameObject fillin; 

    void Start()
    {
        setColor(color, fillin);
        buttonAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPressed && GameManager.Instance.isLowGravity)
        {
            ReleaseButton();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isPressed && other.attachedRigidbody != null && other.CompareTag("Box"))
        {
            // float weight = other.attachedRigidbody.mass * -Physics.gravity.y;
            // if (weight >= weightThreshold)
            if (!GameManager.Instance.isLowGravity)
            {
                PressButton();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed)
        {
            ReleaseButton();
        }
    }

    void PressButton()
    {
        isPressed = true;
        buttonAnimator?.SetTrigger("Press");
        foreach (WalledDoor door in connectedDoors)
        {
            door.OpenDoor();
            door.GetComponent<BoxCollider>().enabled = false;
        }
        onPress.Invoke();
    }

    void ReleaseButton()
    {
        isPressed = false;
        buttonAnimator?.SetTrigger("Release");
        foreach (WalledDoor door in connectedDoors)
        {
            door.CloseDoor();
            door.GetComponent<BoxCollider>().enabled = true;
        }
        onRelease.Invoke();
    }

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
        props.SetColor(Color1, startColor);
        fillin.GetComponent<Renderer>().SetPropertyBlock(props);
    }
}

