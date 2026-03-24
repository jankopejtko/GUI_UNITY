using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager_example : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("level_final_example");
    }

    public void QuitGame()
    {
        Debug.Log("Hra se vypíná...");
        Application.Quit();
    }
}
