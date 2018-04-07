using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
  public float FalloutDelaySecs;

  private Rigidbody _rigidbody;

  public void Update()
  {
    if ((Time.time > FalloutDelaySecs) && (_rigidbody == null))
    {
      _rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
      _rigidbody.mass = 1.0f;
    }
  }
}
