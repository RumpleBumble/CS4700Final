using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Switch : MonoBehaviour
{
    public float lowGravity = 4.9f;   
    public float highGravity = 19.6f;
    private bool playerInRange = false;

    public Canvas canvas;

    public TMP_Text gravityText;

    [SerializeField] private AudioClip lever;

    private void Update()
    {

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"E pressed - Current gravity: {Physics.gravity.y}");
            SFXManager.instance.PlaySFXClip(lever, transform, 0.5f);
            GameManager.Instance.ToggleGravityMode();
            float newGravity = GameManager.Instance.isLowGravity ? lowGravity : highGravity;
            Physics.gravity = new Vector3(0, -newGravity, 0);
            Debug.Log($"Gravity changed to: {Physics.gravity.y}");
            gravityText.text = $"Current gravity: {Physics.gravity.y}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered trigger zone");
            canvas.enabled = true;
        }
    }

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