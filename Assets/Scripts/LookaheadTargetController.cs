using System.Collections;
using UnityEngine;
public class LookaheadTargetController : MonoBehaviour
{
  [SerializeField]
  bool followX = true;

  [SerializeField]
  bool followY = true;

  [SerializeField]
  Vector3 distance = Vector3.one;
  // float distance = 4;


  [SerializeField]
  bool centerWhenStill = true;

  [SerializeField]
  float delayBeforeMoving = .5f;

  [SerializeField]
  float delayBeforeStopping = .2f;

  CharacterController charController;
  Vector3 targetPosition;

  bool wasMoving = false;
  bool isActive = false;

  public bool CurrentlyMoving
  {
    get =>
      (followX && charController.velocity.x != 0) ||
      (followY && charController.velocity.y != 0);
  }

  public Vector3 Direction
  {
    get => new Vector3(
      followX ? charController.velocity.x : 0,
      followY ? charController.velocity.y : 0
    ).normalized;
  }

  void Start()
  {
    charController = transform.parent.GetComponent<CharacterController>();
  }

  void Update()
  {
    if (CurrentlyMoving && !wasMoving)
    {
      StartCoroutine(DelayActivate());
    }

    if (!CurrentlyMoving && wasMoving)
    {
      StartCoroutine(DelayDeactivate());
    }

    // Activity when moving
    if (isActive)
    {
      targetPosition = new(
        Direction.x * distance.x,
        Direction.y * distance.y,
        Direction.z * distance.z
      );
    }
    // Activity at rest
    else if (centerWhenStill)
    {
      targetPosition = Vector3.zero;
    }

    transform.localPosition = targetPosition;

    wasMoving = CurrentlyMoving;
  }

  IEnumerator DelayActivate()
  {
    yield return new WaitForSeconds(delayBeforeMoving);
    if (CurrentlyMoving) isActive = true;
    else isActive = false;
  }

  IEnumerator DelayDeactivate()
  {
    yield return new WaitForSeconds(delayBeforeStopping);
    if (!CurrentlyMoving) isActive = false;
  }
}