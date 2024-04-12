using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsJumpController : JumpController
{
  [SerializeField]
  float jumpHeight = 7;

  [SerializeField]
  float fallGravityScale = 12;

  float defaultGravityScale;

  Rigidbody2D rBody;

  GroundCheckController groundCheckController;

  void Awake()
  {
    rBody = GetComponent<Rigidbody2D>();
    groundCheckController = GetComponent<GroundCheckController>();

    defaultGravityScale = rBody.gravityScale;
  }

  void Update()
  {
    if (rBody.velocity.y < 0)
    {
      rBody.gravityScale = fallGravityScale;
      CurrentState = JumpState.Falling;
    }
    else
    {
      rBody.gravityScale = defaultGravityScale;

      if (rBody.velocity.y > 0)
      {
        CurrentState = JumpState.Jumping;
      }
      else
      {
        CurrentState = JumpState.Idle;
      }
    }
  }

  void OnJump(InputValue inputValue)
  {
    if (inputValue.Get<float>() > 0 && groundCheckController.CurrentState == GroundCheckController.GroundState.Grounded)
    {
      float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rBody.gravityScale) * -2 * rBody.mass);

      rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    else
    {
      
    }
  }
}
