using System.Collections;
using System.Collections.Generic;
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
    } else if (other.gameObject.CompareTag("ZeroLevel")) {
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
    var scene = SceneManager.GetSceneByName("MiniGame");
    SceneManager.LoadScene(scene.buildIndex);
  }

}
