using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("MiniGame");
        Debug.Log("LoadScene(\"MiniGame\")");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}