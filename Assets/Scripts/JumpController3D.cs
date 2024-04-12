using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController3D))]
public class JumpController3D : MonoBehaviour
{
  [SerializeField]
  float jumpForce = 35;

  [SerializeField]
  float coyoteTime = 0.5f;

  [SerializeField]
  [Range(0.0f, 1.0f)]
  float cancelJumpRatio = 0.3f;

  private bool jumpNow = false;
  private bool coyoteActive = false;

  private CharacterController charController;
  private PlayerController3D playerController;

  public Action OnJumpAction;

  private bool extraJump = false;

  private void Awake()
  {
    playerController = GetComponent<PlayerController3D>();
    charController = GetComponent<CharacterController>();

    playerController.OnLeaveGround += EnCoyote;
  }

  private void Update()
  {
    if ((charController.isGrounded || coyoteActive || extraJump) && jumpNow)
    {
      playerController.VerticalVelocity = jumpForce;
      if (extraJump) extraJump = false;
      OnJumpAction?.Invoke();
    }
    jumpNow = false;
  }

  void OnJump(InputValue value)
  {
    if (value.Get<float>() > 0) jumpNow = true;
    else if (playerController.VerticalVelocity > 0)
      playerController.VerticalVelocity *= cancelJumpRatio;
  }

  void EnCoyote()
  {
    if (playerController.VerticalVelocity < 0)
    {
      coyoteActive = true;
      Invoke("DeCoyote", coyoteTime);
    }
  }

  void DeCoyote() => coyoteActive = false;

  public void AllowExtraJump() => extraJump = true;
}
