using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

  [SerializeField] private float _speed;

  private Vector2 worldMousePosition;
  private Vector3 target;
  [SerializeField] public float _speedToMouse = 5f;

  [SerializeField] private float _rotationSpeed;
  [SerializeField] private float _screenBorder;
  private Rigidbody2D _rigidbody;
  private Vector2 _movementInput;
  private Vector2 _smoothedMovementInput;
  private Vector2 _movementInputSmoothVelocity;
  private Camera _camer;

  [SerializeField] private Transform _tongue;
  [SerializeField] private Transform _tongueLine;
  private Boolean _isFire = false;

  private void Awake()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
    _camer = Camera.main;
  }



  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("Collision accured with ", collision.gameObject);
    if (collision.GetComponent<EnemyMovement>())
    {

      Destroy(collision.gameObject);
    }
  }



  void Start()
  {
    target = transform.position;
  }
  private void Update()
  {
    if (Input.GetMouseButtonDown(1))
    {
      _isFire = false;
      target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      target.z = transform.position.z;

    }
    if (Input.GetMouseButtonDown(0))
    {
      _isFire = true;
    }

    transform.position = Vector3.MoveTowards(transform.position, target, _speedToMouse * Time.deltaTime);
    PreventPlayerGoingOffScreen();
    if (!_isFire)
    {
      _tongue.position = transform.position;

    }

    _tongueLine.position = transform.position;





  }


  private void PreventPlayerGoingOffScreen()
  {
    Vector2 screenPosition = _camer.WorldToScreenPoint(transform.position);
    if ((screenPosition.x < _screenBorder && _rigidbody.velocity.x < 0) ||
     (screenPosition.x > _camer.pixelWidth - _screenBorder && _rigidbody.velocity.x > 0))
    {
      _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }
    if ((screenPosition.y < _screenBorder && _rigidbody.velocity.y < 0) ||
     (screenPosition.y > _camer.pixelHeight - _screenBorder && _rigidbody.velocity.y > 0))
    {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
    }
  }

  private void FixedUpdate()
  {
    SetPlayerVelocity();
    //RotateInDirectionOfInput();

  }

  private void SetPlayerVelocity()
  {

    _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);
    _rigidbody.velocity = _smoothedMovementInput * _speed;
    PreventPlayerGoingOffScreen();

  }

  private void RotateInDirectionOfInput()
  {

    if (_movementInput != Vector2.zero)
    {

      Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
      Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
      _rigidbody.MoveRotation(rotation);
    }

  }


  private void OnMove(InputValue inputValue)
  {
    _movementInput = inputValue.Get<Vector2>();

  }




}
