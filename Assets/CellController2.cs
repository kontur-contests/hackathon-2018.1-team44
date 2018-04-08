using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController2 : MonoBehaviour
{
  public float FalloutDelaySecs;

  private float _falloutTime;

  private Rigidbody _rigidbody;
  private WorldController _world;

  public void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
    _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    _falloutTime = Time.time + FalloutDelaySecs;

    var worldObject = GameObject.Find("World");
    _world = worldObject.GetComponent<WorldController>();
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
      _world.CellCount -= 1;
      gameObject.SetActive(false);
    }
  }
}
