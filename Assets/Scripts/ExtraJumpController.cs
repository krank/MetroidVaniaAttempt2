using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpController3D))]
[RequireComponent(typeof(PlayerController3D))]
public class ExtraJumpController : MonoBehaviour
{
  [SerializeField]
  private int JumpsAllowed = 2;

  private int JumpsMade = 0;

  private void Awake()
  {
    GetComponent<JumpController3D>().OnJumpAction += HandleJump;
    GetComponent<PlayerController3D>().OnLand += ResetExtraJumps;
  }

  private void HandleJump()
  {
    if (JumpsMade < JumpsAllowed) JumpsMade++;
    // Allow another jump
    if (JumpsMade < JumpsAllowed) GetComponent<JumpController3D>().AllowExtraJump();
  }

  private void ResetExtraJumps() => JumpsMade = 0;

}
