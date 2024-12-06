/***************************************************************
*file: CameraFollow.cs
*author: Jack Peng
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 12/2/2024
*
*purpose: This lets the camera follow the movement of the player
*
****************************************************************/

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;                             // How far away the camera is to the player
    [SerializeField] private Transform target;          // The position of the player
    [SerializeField] private float smoothTime;          // Used to smooth the movement of the camera
    private Vector3 currentVelocity;                    // How fast the the camera is currently moving

    /**
     * Initializes the camera's position
     */
    private void Awake()
    {
        offset = transform.position - target.position;
        currentVelocity = Vector3.zero;
    }

    /**
     * Updates the camera's position based on the player's current position/movement
     */
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
