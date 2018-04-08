using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
  public int CellCount;

  private PlayerController _player;
  private LevelGenerator _levelGenerator;
  private Text _finalText;
  private float _nextStarSpawnTime;


  // Use this for initialization
  void Start()
  {
    var levelGeneratorObject = GameObject.Find("LevelGenerator");
    _levelGenerator = levelGeneratorObject.GetComponent<LevelGenerator>();
    CellCount = _levelGenerator.LevelWidth * _levelGenerator.LevelHeight;

    var playerObject = GameObject.Find("RollerBall");
    _player = playerObject.GetComponent<PlayerController>();

    var finalTextObject = GameObject.Find("FinalText");
    _finalText = finalTextObject.GetComponent<Text>();
    _finalText.text = "";

    _nextStarSpawnTime = Time.time + 5.0f;    
  }

  // Update is called once per frame
  void Update()
  {
    if (Time.time > _nextStarSpawnTime)
    {
      float offsetX = Random.Range(-4.0f, +4.0f);
      float offsetZ = Random.Range(-4.0f, +4.0f);

      var starPosition = _player.transform.position;
      starPosition.x += offsetX;
      starPosition.y += 1.0f;
      starPosition.z += offsetZ;
      _levelGenerator.SpawnStar(starPosition);
      Debug.Log("Star spawed");
      _nextStarSpawnTime = Time.time + 5.0f;
    }

    if (CellCount == 1)
    {
      _finalText.text = "Congratulations!";
    }
  }
}
