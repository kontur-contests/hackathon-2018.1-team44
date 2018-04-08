using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
  public int Power;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("ZeroLevel"))
    {
      gameObject.SetActive(false);
    }
  }
}
