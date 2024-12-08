/***************************************************************
*file: ExitDoor.cs
*author: Andy Nguyen
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 12/3/2024
*
*purpose: Moves to the next level upon touching
*
****************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public bool nexLow; // If the next level starts with low gravity
    [SerializeField] private string nextSceneName; // Name of the next level
    
    // Moves to the next level upon player touching
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isLowGravity = nexLow;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}