using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckController : MonoBehaviour
{
  public enum GroundState { Grounded, NotGrounded }

  public GroundState CurrentState { get; set; } = GroundState.NotGrounded;

  [SerializeField]
  protected LayerMask groundLayers;
}
