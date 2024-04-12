using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class JumpController : MonoBehaviour
{
  public enum JumpState {Idle, Jumping, Falling}

  public JumpState CurrentState { get; set; }
}
