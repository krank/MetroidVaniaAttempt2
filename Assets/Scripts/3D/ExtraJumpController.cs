using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vania3D
{
  [RequireComponent(typeof(JumpController))]
  [RequireComponent(typeof(PlayerController))]
  public class ExtraJumpController : MonoBehaviour
  {
    [SerializeField]
    private int JumpsAllowed = 2;

    private int JumpsMade = 0;

    private void Awake()
    {
      GetComponent<JumpController>().OnJumpAction += HandleJump;
      GetComponent<PlayerController>().OnLand += ResetExtraJumps;
    }

    private void HandleJump()
    {
      if (JumpsMade < JumpsAllowed) JumpsMade++;
      // Allow another jump
      if (JumpsMade < JumpsAllowed) GetComponent<JumpController>().AllowExtraJump();
    }

    private void ResetExtraJumps() => JumpsMade = 0;

  }
}