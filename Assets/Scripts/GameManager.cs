using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;
    public bool isLowGravity = true;
    public Text gravityIndicator;
    public int currentLevel = 1;
    public int totalLevels = 4;

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
    }

    void UpdateGravityIndicator()
    {
        if (gravityIndicator != null)
        {
            gravityIndicator.text = "Gravity: " + (isLowGravity ? "Low" : "High");
        }
    }

    public void NextLevel()
    {
        if (currentLevel < totalLevels)
        {
            currentLevel++;
            SceneManager.LoadScene("Level" + currentLevel);
        }
        else
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}