/***************************************************************
*file: GameManager.cs
*author: Emiley Thai
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 11/20/2024
*
*purpose: This manages the level's gravity level so all objects
*         in the scene can reference this
*
****************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance; // Game instance
    public bool isLowGravity;           // If the gravity level is low. Initially set in the level scene
    public Text gravityIndicator;       // Where the UI states the current gravity level

    /**
     * Creates an instance and keep it across scenes
     */
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

    /**
     * Sets gravity UI to starting level
     */
    void Start()
    {
        UpdateGravityIndicator();
    }

    /**
     * Change gravity mode and updates UI
     */
    public void ToggleGravityMode()
    {
        isLowGravity = !isLowGravity;
        UpdateGravityIndicator();
    }

    /**
     * Updates gravity UI
     */
    void UpdateGravityIndicator()
    {
        if (gravityIndicator != null)
        {
            gravityIndicator.text = "Gravity: " + (isLowGravity ? "Low" : "High");
        }
    }
}