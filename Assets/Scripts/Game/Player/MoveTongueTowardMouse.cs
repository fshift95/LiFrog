using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MoveTongueTowardMouse : MonoBehaviour
{


  private LineRenderer lr;

  private bool readyToFire = false;
  private bool _isComeBack = false;
  private bool _hadComeBack = false;
  [SerializeField] private double _allowedDistanceToTarget = 1.1;


  //public GameObject shootingS;


  private Vector3 _firstpostition;
  private Vector3 target;
  private Animator _animator;
  private Vector2 _direction;
  [SerializeField] public float _speedToMouse = 15f;

  public float strength = 1;

  private GameObject stickedEnemy;
  private bool isSticked = false;
  private bool stepBackward = false;
  private bool _checkCollision = false;
  // Start is called before the first frame update
  void Start()
  {
    lr = GetComponent<LineRenderer>();

    target = transform.position;


  }


  public Vector2 finalPosition;
  public void resetStrength()
  {
    strength = 0;
  }


  // Gets called during the collision





  private void OnTriggerEnter2D(Collider2D collision)
  {

    if (Vector3.Distance((_firstpostition + direction), transform.position) < _allowedDistanceToTarget)
    {
      if (collision.GetComponent<EnemyMovement>())
      {
        stickedEnemy = collision.gameObject;
        isSticked = true;

        _animator = stickedEnemy.GetComponent<Animator>();
        _animator.SetBool("IsDead", true);

      }
      _checkCollision = false;
    }






  }
  private Vector3 direction;
  private bool fire = true;
  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButton(0)) // strength by time
    {
      Debug.Log("prepare to shoot");
      readyToFire = false;   // do not fire 
      if (strength < 20)
      {
        strength += Time.deltaTime * 10;
      }
      Vector3 pp = gameObject.transform.position;
      Vector3[] positions = new Vector3[2];
      positions[0] = new Vector3(-6, -4);
      positions[1] = new Vector3(-6, -4 + strength / 10);
      lr.positionCount = positions.Length;
      lr.SetPositions(positions);
    }
    else
    {
      if (strength > 1)
      {
        Debug.Log("ready to shoot");
        readyToFire = true; // do not fire 
        fire = true;
      }

    }
    if (readyToFire && fire)
    {  // set direction
      Debug.Log("gggfirrrrrrrreee");
      target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      target.z = 0;
      direction = target - gameObject.transform.parent.position;
      _firstpostition = gameObject.transform.position;
      direction = Vector3.ClampMagnitude(direction, 1) * strength;
      fire = false;
    }
    if (readyToFire && !_isComeBack && direction != Vector3.zero)
    {

      transform.position = Vector3.MoveTowards(transform.position, _firstpostition + direction, _speedToMouse * Time.deltaTime);
      strength = 1;
    }
    if ((transform.position == (_firstpostition + direction)) && direction != Vector3.zero)
    {

      _isComeBack = true;
      stepBackward = true;
    }
    if (stepBackward)
    {

      transform.position = Vector3.MoveTowards(transform.position, _firstpostition, _speedToMouse * Time.deltaTime);



      if (transform.position == _firstpostition)
      {

        direction = Vector3.zero;
        _isComeBack = false;
        stepBackward = false;
        Debug.Log("came backkkkk");
      }
    }
    if (isSticked)
    {
      if (stickedEnemy)
      {

        stickedEnemy.transform.position = this.transform.position;
      }
    }

  }
}
