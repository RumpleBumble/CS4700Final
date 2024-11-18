using UnityEngine;
using UnityEngine.UI; // For UI elements

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isLowGravity = true;

    public Text gravityIndicator; // Reference to the UI Text element

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateGravityIndicator();
    }

    public void ToggleGravityMode()
    {
        isLowGravity = !isLowGravity;
        UpdateGravityIndicator();
        Debug.Log("Gravity Mode: " + (isLowGravity ? "Low Gravity" : "High Gravity"));
    }

    void UpdateGravityIndicator()
    {
        if (gravityIndicator != null)
        {
            gravityIndicator.text = "Gravity: " + (isLowGravity ? "Low" : "High");
        }
    }
}
