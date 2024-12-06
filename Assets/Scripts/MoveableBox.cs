/***************************************************************
*file: MoveableBox.cs
*author: Emiley Thai
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 12/3/2024
*
*purpose: This manages being able to pick up and move the box
*
****************************************************************/

using UnityEngine;

public class MoveableBox : MonoBehaviour
{
    private GameObject heldObject;       // The object being held
    private Collider heldObjectCollider; // Store reference to the held object's collider
    
    // Parameters for object pick-up dimensions
    private const float RADIUS = 0.5f;   // Reduced radius for more precise pickup
    private const float DISTANCE = 2f;
    private const float HEIGHT = 1f;
    private const float PICKUP_RANGE = 2f;

    //  Handles the pickup per frame
    private void Update()
    {
        var t = transform;                       // Position of held object
        var pressedF = Input.GetKeyDown(KeyCode.F);  // Button for picking up
        
        if (heldObject) // If the player is currently holding an object
        {
            // Handle dropping the object
            if (pressedF)
            {
                var rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
                if (heldObjectCollider != null)
                {
                    heldObjectCollider.enabled = true; // Re-enable collider when dropping
                }
                heldObject = null;
                heldObjectCollider = null;
                return;
            }

            // Update held object position with identity rotation (no rotation)
            Vector3 targetPosition = t.position + (t.forward * DISTANCE) + (t.up * HEIGHT);
            heldObject.transform.position = targetPosition;
            heldObject.transform.rotation = Quaternion.identity;
        }
        else if (pressedF)
        {
            // Cast a ray forward from the player's position
            Ray ray = new Ray(t.position + t.up, t.forward);
            RaycastHit[] hits = Physics.SphereCastAll(
                ray.origin,
                RADIUS,
                ray.direction,
                PICKUP_RANGE
            );

            if (hits.Length > 0) // The ray hits an object
            {   // Determines if the object is a box
                var hitIndex = System.Array.FindIndex(hits, hit => 
                    hit.transform != null && 
                    hit.transform != transform &&
                    hit.transform.CompareTag("Box")
                );
                
                // Now we set the held object to the box if it is one
                if (hitIndex != -1 && hitIndex < hits.Length)
                {
                    var hitTransform = hits[hitIndex].transform;
                    if (hitTransform != null)
                    {
                        var hitObject = hitTransform.gameObject;
                        if (hitObject != gameObject)
                        {
                            heldObject = hitObject;
                            
                            // Store and disable the collider
                            heldObjectCollider = hitObject.GetComponent<Collider>();
                            if (heldObjectCollider != null)
                            {
                                heldObjectCollider.enabled = false;
                            }
                            
                            var rb = hitObject.GetComponent<Rigidbody>();
                            if (rb != null)
                            {
                                rb.isKinematic = true;
                            }
                            
                            hitObject.transform.rotation = Quaternion.identity;
                        }
                    }
                }
            }
        }
    }

    /**
     * Removes a held object if we dropped it
     */
    private void OnDisable()
    {
        if (heldObject != null)
        {
            var rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            if (heldObjectCollider != null)
            {
                heldObjectCollider.enabled = true;
            }
            heldObject = null;
            heldObjectCollider = null;
        }
    }

    // Optional: Visualize the pickup range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 forward = transform.forward * PICKUP_RANGE;
        Gizmos.DrawWireSphere(transform.position + transform.up, RADIUS);
        Gizmos.DrawLine(transform.position + transform.up, transform.position + transform.up + forward);
        Gizmos.DrawWireSphere(transform.position + transform.up + forward, RADIUS);
    }
}