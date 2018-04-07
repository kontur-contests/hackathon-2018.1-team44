using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class PlayerController : BallUserControl
{
    public Text ScoreText;

    private int _score;

    private void Start()
    {
        _score = 0;
        ScoreText.text = string.Format("Score: {0}", _score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            OnStarCollided(other.gameObject);
        }
        else if (other.gameObject.CompareTag("ZeroLevel"))
        {
            OnZeroLevelCollided();
        }
    }

    private void OnStarCollided(GameObject star)
    {
        star.SetActive(false);
        _score += 1;
        ScoreText.text = string.Format("Score: {0}", _score);
    }

    private void OnZeroLevelCollided()
    {
        // ХЗ почему игра не видит имя сцены. Если вывести его на консоль, то будет пустая строка. 
        //var scene = SceneManager.GetSceneByName("Menu");
        //SceneManager.LoadScene(scene.buildIndex);

        var scene = SceneManager.GetSceneByBuildIndex(0);
        Debug.Log("Need go to menu on buildScene " + scene.name);
        SceneManager.LoadScene(0);
    }

}
