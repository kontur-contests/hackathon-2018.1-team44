using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  public GameObject player;
  public Vector3 Offset;

  private Vector3 cameraTranslation;


  // Use this for initialization
  void Start()
  {
    Offset = new Vector3(0.0f, 0.0f, 0.0f);
    cameraTranslation = transform.position - player.transform.position;
    transform.position = player.transform.position + cameraTranslation;
  }

  private void Update()
  {
    //float yaw = 0.0f;
    //float pitch = 0.0f;

    //if (Input.GetKey(KeyCode.A))
    //  yaw = 1.0f;
    //else if (Input.GetKey(KeyCode.D))
    //  yaw = -1.0f;

    //if (Input.GetKey(KeyCode.W))
    //  pitch = 1.0f;
    //else if (Input.GetKey(KeyCode.S))
    //  pitch = -1.0f;

    transform.position = player.transform.position + cameraTranslation + Offset;
    //transform.RotateAround(player.transform.position, new Vector3(pitch, yaw, 0.0f), 20 * Time.deltaTime * 8.0f);
    transform.LookAt(player.transform.position);
  }

  // Update is called once per frame
  void LateUpdate()
  {
    //transform.position = player.transform.position + offset;
  }
}
