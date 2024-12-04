using UnityEngine;

public class MoveableBox : MonoBehaviour
{
    private GameObject heldObject;
    private Collider heldObjectCollider; // Store reference to the held object's collider
    public float radius = 0.5f;          // Reduced radius for more precise pickup
    public float distance = 2f;
    public float height = 1f;
    public float pickupRange = 2f;

    private void Update()
    {
        var t = transform;
        var pressedF = Input.GetKeyDown(KeyCode.F);
        
        if (heldObject)
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
            Vector3 targetPosition = t.position + (t.forward * distance) + (t.up * height);
            heldObject.transform.position = targetPosition;
            heldObject.transform.rotation = Quaternion.identity;
        }
        else if (pressedF)
        {
            // Cast a ray forward from the player's position
            Ray ray = new Ray(t.position + t.up, t.forward);
            RaycastHit[] hits = Physics.SphereCastAll(
                ray.origin,
                radius,
                ray.direction,
                pickupRange
            );

            if (hits.Length > 0)
            {
                var hitIndex = System.Array.FindIndex(hits, hit => 
                    hit.transform != null && 
                    hit.transform != transform &&
                    hit.transform.CompareTag("Box")
                );
                
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
        Vector3 forward = transform.forward * pickupRange;
        Gizmos.DrawWireSphere(transform.position + transform.up, radius);
        Gizmos.DrawLine(transform.position + transform.up, transform.position + transform.up + forward);
        Gizmos.DrawWireSphere(transform.position + transform.up + forward, radius);
    }
}