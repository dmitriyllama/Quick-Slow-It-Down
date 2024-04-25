using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void OnNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnOpenSettings()
    {
        
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
