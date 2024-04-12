using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbiter : MonoBehaviour
{
  [SerializeField]
  Transform target;

  void Update()
  {
    transform.RotateAround(target.position, Vector2.up, Time.deltaTime * 10);
    }
}
