using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController2 : MonoBehaviour
{
  public float FalloutDelaySecs;

  private float _falloutTime;

  private Rigidbody _rigidbody;

  public void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
    _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    _falloutTime = Time.time + FalloutDelaySecs;
  }

  public void Update()
  {
    if (Time.time > _falloutTime)
    {
      _rigidbody.useGravity = true;
      _rigidbody.constraints = RigidbodyConstraints.None;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("ZeroLevel"))
    {
      gameObject.SetActive(false);
    }
  }
}
