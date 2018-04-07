using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightController : MonoBehaviour
{
  private Light _light;

  // Use this for initialization
  void Start()
  {
    _light = GetComponent<Light>();
  }

  // Update is called once per frame
  void Update()
  {
    if (_light != null)
    {
      //int thunderValue = Random.Range(0, 100);
      //if (thunderValue > 80)
      //  _light.intensity = 2.0f;
    }
  }
}
