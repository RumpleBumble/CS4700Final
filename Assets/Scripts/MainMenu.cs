using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("test"); // Replace "PuzzleRoom1" with your game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit(); // Won't work in the editor, but will in a build
    }
}
