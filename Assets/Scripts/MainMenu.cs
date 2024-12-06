/***************************************************************
*file: MainMenu.cs
*author: Richard Corvera
*class: CS 4700 – Game Development
*assignment: Program 4
*date last modified: 11/30/2024
*
*purpose: This manages the start up menu
*
****************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /**
     * When the play game button is selected
     */
    public void PlayGame()
    {
        SceneManager.LoadScene("level1"); // Replace "PuzzleRoom1" with your game scene name
    }

    /**
     * When the quit game button is selected
     */
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit(); // Won't work in the editor, but will in a build
    }
}
