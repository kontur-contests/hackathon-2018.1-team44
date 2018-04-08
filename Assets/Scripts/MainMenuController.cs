using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
  public void Start()
  {
    if (RenderSettings.skybox.HasProperty("_Tint"))
      RenderSettings.skybox.SetColor("_Tint", Color.gray);
    else if (RenderSettings.skybox.HasProperty("_SkyTint"))
      RenderSettings.skybox.SetColor("_SkyTint", Color.gray);
  }

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