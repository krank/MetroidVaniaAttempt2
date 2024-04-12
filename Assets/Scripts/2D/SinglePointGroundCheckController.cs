using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePointGroundCheckController : GroundCheckController
{
  [SerializeField]
  float feetRadius = 0.1f;

  void FixedUpdate()
  {
    if (Physics2D.OverlapCircle(GetFeetPosition(), feetRadius, groundLayers))
    {
      CurrentState = GroundState.Grounded;
    }
    else
    {
      CurrentState = GroundState.NotGrounded;
    }
    print(CurrentState);
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(GetFeetPosition(), feetRadius);
  }

  Vector2 GetFeetPosition()
  {
    return new Vector2(
      GetComponent<Collider2D>().bounds.center.x,
      GetComponent<Collider2D>().bounds.min.y
    );
  }
}
