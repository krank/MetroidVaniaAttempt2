using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// https://www.youtube.com/watch?v=c9kxUvCKhwQ

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

  [SerializeField]
  float speed = 4;

  Vector2 movement;
  Rigidbody2D rBody;

  void Awake()
  {
    rBody = GetComponent<Rigidbody2D>();
  }

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.Translate(movement * speed * Time.deltaTime);

    // rBody.velocity = new Vector2(
    //   movement.x * speed,
    //   rBody.velocity.y
    // );
  }

  void OnMove(InputValue inputValue)
  {
    movement = new Vector2(inputValue.Get<Vector2>().x, 0);
  }
}
