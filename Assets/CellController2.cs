using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController2 : MonoBehaviour
{
  public float FalloutDelaySecs;

  private Rigidbody _rigidbody;

  public void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
    _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
  }

  public void Update()
  {
    if (Time.time > FalloutDelaySecs)
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
