using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
  [SerializeField]
  GameObject markerPrefab;

  public float VerticalVelocity { get; set; }

  [SerializeField]
  float speed = 3;

  [SerializeField]
  float gravityMultiplier;

  private bool wasGrounded;

  CharacterController charController;

  Vector2 movementVector;

  // Public subscribable events
  public Action OnLeaveGround;
  public Action OnLand;

  void Awake()
  {
    charController = GetComponent<CharacterController>();
  }

  void Update()
  {
    ApplyGravity();

    if (charController.isGrounded && VerticalVelocity < 0)
    {
      ResetVelocity();
    }
    
    Vector3 movement = new()
    {
      x = movementVector.x * speed,
      y = VerticalVelocity
    };

    // Final move
    charController.Move(movement * Time.deltaTime);

    // Groundedness
    if (wasGrounded && !charController.isGrounded) OnLeaveGround?.Invoke();
    
    if (!wasGrounded && charController.isGrounded) OnLand?.Invoke();

    wasGrounded = charController.isGrounded;
  }

  void ApplyGravity() => VerticalVelocity += Physics.gravity.y * Time.deltaTime * gravityMultiplier;

  void OnMove(InputValue value) => movementVector = value.Get<Vector2>();

  void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.normal.y < -0.1 && VerticalVelocity > 0)
    {
      ResetVelocity();
      // Instantiate(markerPrefab, hit.point, Quaternion.identity);
    }
  }

  public void ResetVelocity() => VerticalVelocity = -0.2f;

}
