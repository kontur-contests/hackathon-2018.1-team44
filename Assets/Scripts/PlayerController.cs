using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class PlayerController : BallUserControl
{
  public Text ScoreText;

  private int _score;
  private float _berserkTimeout;

  private Skybox _cameraSkybox;
  private AudioSource _playerAudioSource;
  private AudioSource _cameraAudioSource;
  private CameraController _cameraController;
  private ParticleSystem _rainSystem;
  private ParticleSystem _ashSystem;
  private Light _globalLight;

  public bool IsBerserkModeEnabled
  {
    get { return _berserkTimeout > 0.0f; }
  }

  private void Start()
  {
    _berserkTimeout = 0.0f;
    _score = 0;
    ScoreText.text = string.Format("Score: {0}", _score);

    _cameraController = camera.GetComponent<CameraController>();
    _cameraAudioSource = camera.GetComponent<AudioSource>();
    _cameraSkybox = camera.GetComponent<Skybox>();
    if (_cameraSkybox.material.HasProperty("_Tint"))
      _cameraSkybox.material.SetColor("_Tint", Color.gray);
    else if (_cameraSkybox.material.HasProperty("_SkyTint"))
      _cameraSkybox.material.SetColor("_SkyTint", Color.gray);

    _playerAudioSource = GetComponent<AudioSource>();

    var rainPlane = GameObject.Find("RainPlane");
    _rainSystem = rainPlane.GetComponent<ParticleSystem>();
    var ashPlane = GameObject.Find("AshPlane");
    _ashSystem = ashPlane.GetComponent<ParticleSystem>();

    _rainSystem.Play();
    _ashSystem.Stop();

    var globalLightObject = GameObject.Find("GlobalLight");
    _globalLight = globalLightObject.GetComponent<Light>();
  }

  protected void Update()
  {
    //Debug.Log(string.Format("_berserkTimeout: {0}", _berserkTimeout));
    if (IsBerserkModeEnabled)
    {
      UpdateBerserkMode();
      if (_berserkTimeout <= 0.0f)
        DisableBerserkMode();
    }

    base.Update();
  }

  private void EnableBerserkMode()
  {
    _cameraAudioSource.Stop();
    _playerAudioSource.Play();
    _rainSystem.Stop();
    _ashSystem.Play();
    //_globalLight.intensity = 0.01f;
    RenderSettings.ambientIntensity = 0.2f;

    if (_cameraSkybox.material.HasProperty("_Tint"))
      _cameraSkybox.material.SetColor("_Tint", Color.red);
    else if (_cameraSkybox.material.HasProperty("_SkyTint"))
      _cameraSkybox.material.SetColor("_SkyTint", Color.red);
    if (RenderSettings.skybox.HasProperty("_Tint"))
      RenderSettings.skybox.SetColor("_Tint", Color.red);
    else if (RenderSettings.skybox.HasProperty("_SkyTint"))
      RenderSettings.skybox.SetColor("_SkyTint", Color.red);
  }

  private void UpdateBerserkMode()
  {
    _berserkTimeout -= Time.deltaTime;

    float cameraVerticalOffset = Random.Range(-0.5f, 0.5f);
    float cameraHorizontalOffset = Random.Range(-0.5f, 0.5f);
    _cameraController.Offset = new Vector3(cameraHorizontalOffset, cameraVerticalOffset, 0.0f);

    const float berserkAreaRadius = 4.0f;
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, berserkAreaRadius);
    foreach (Collider hit in hitColliders)
    {
      if (hit.gameObject.CompareTag("Star"))
      {
        var direction = (transform.position - hit.gameObject.transform.position);
        direction.Normalize();

        var velocity = direction * 2.0f;
        var translation = velocity * Time.deltaTime;
        hit.gameObject.transform.position += translation;
      }
    }
  }

  private void DisableBerserkMode()
  {
    _berserkTimeout = 0.0f;
    _cameraController.Offset = new Vector3(0.0f, 0.0f, 0.0f);
    _cameraAudioSource.Play();
    _playerAudioSource.Stop();
    _rainSystem.Play();
    _ashSystem.Stop();
    RenderSettings.ambientIntensity = 1.0f;
    //_globalLight.intensity = 0.5f;

    if (_cameraSkybox.material.HasProperty("_Tint"))
       _cameraSkybox.material.SetColor("_Tint", Color.gray);
    else if (_cameraSkybox.material.HasProperty("_SkyTint"))
        _cameraSkybox.material.SetColor("_SkyTint", Color.gray);
    if (RenderSettings.skybox.HasProperty("_Tint"))
      RenderSettings.skybox.SetColor("_Tint", Color.gray);
    else if (RenderSettings.skybox.HasProperty("_SkyTint"))
      RenderSettings.skybox.SetColor("_SkyTint", Color.gray);
  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log(other.gameObject.tag);
    if (other.gameObject.CompareTag("Star"))
    {
      OnStarCollided(other.gameObject);
    }
    else if (other.gameObject.CompareTag("ZeroLevel"))
    {
      OnZeroLevelCollided();
    }
    else if (other.gameObject.CompareTag("Mushroom"))
    {
      OnMushroomCollided(other.gameObject);
    }
  }

  private void OnStarCollided(GameObject star)
  {
    star.SetActive(false);
    _score += 1;
    ScoreText.text = string.Format("Score: {0}", _score);
  }

  private void OnMushroomCollided(GameObject mushroom)
  {
    Debug.Log("OnMushroomCollided");
    var mushroomController = mushroom.GetComponent<MushroomController>();
    if (!IsBerserkModeEnabled)
      EnableBerserkMode();
    _berserkTimeout += 5.0f * mushroomController.Power;

    mushroom.SetActive(false);
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
