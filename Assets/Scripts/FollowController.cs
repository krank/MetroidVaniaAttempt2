using UnityEngine;

namespace Vania3D
{
  public class FollowController : MonoBehaviour
  {
    [SerializeField]
    Transform targetObject;

    [SerializeField]
    Transform origoObject;

    [Header("Following")]
    [SerializeField]
    bool followX = true;
    [SerializeField]
    bool followY = true;

    [SerializeField]
    float maxSpeed = 1;

    [Header("Easing")]
    [SerializeField]
    float maxEasingDistance = 3;
    [SerializeField]
    AnimationCurve easingCurve;

    Vector3 offsetLastFrame;

    void Start()
    {
      SetOffset();
    }

    void Update()
    {
      ApplyOffset();

      Vector3 targetPos = new(
        followX ? targetObject.position.x : transform.position.x,
        followY ? targetObject.position.y : transform.position.y,
        transform.position.z
      );

      // Difference between where we want to be and where we actually are
      Vector3 diff = new(
        followX ? transform.position.x - targetObject.position.x : 0,
        followY ? transform.position.y - targetObject.position.y : 0
      );

      float distanceBetween = Mathf.Min(maxEasingDistance, diff.magnitude);

      // Acceleration based on how far aling the maxEasingDistance we've gotten
      float acceleration = easingCurve.Evaluate(1f - (distanceBetween / maxEasingDistance));

      transform.position = Vector3.MoveTowards(
        transform.position,
        targetPos,
        maxSpeed * acceleration * Time.deltaTime
      );

      SetOffset();
    }

    void SetOffset()
    {
      offsetLastFrame = transform.position - origoObject.position;
    }

    void ApplyOffset()
    {
      // transform.position = origoObject.position + offsetLastFrame;
      transform.position = new(
        followX ? origoObject.position.x + offsetLastFrame.x : transform.position.x,
        followY ? origoObject.position.y + offsetLastFrame.y : transform.position.y,
        transform.position.z
      );
    }
  }
}