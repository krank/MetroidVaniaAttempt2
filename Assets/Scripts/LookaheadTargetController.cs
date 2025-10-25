using UnityEngine;
namespace Vania3D
{
  public class LookaheadTargetController : MonoBehaviour
  {

    [SerializeField]
    bool followX = true;

    [SerializeField]
    bool followY = true;

    [SerializeField]
    float distance = 4;

    [SerializeField]
    bool centerWhenStill = true;

    CharacterController charController;
    Vector3 targetPosition;

    void Start()
    {
      charController = transform.parent.GetComponent<CharacterController>();
    }

    void Update()
    {
      Vector3 direction = new Vector3(
        followX ? charController.velocity.x : 0,
        followY ? charController.velocity.y : 0
      ).normalized;

      bool currentlyMoving =
        (followX && charController.velocity.x != 0) ||
        (followY && charController.velocity.y != 0);

      // Activity when moving
      if (currentlyMoving)
      {
        targetPosition = direction * distance;
      }

      // Activity at rest
      else if (centerWhenStill)
      {
        targetPosition = Vector3.zero;
      }

      transform.localPosition = targetPosition;
    }
  }
}