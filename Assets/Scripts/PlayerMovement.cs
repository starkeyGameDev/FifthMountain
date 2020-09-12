using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 6f;
  public float turnSpeed = .1f;

  CharacterController controller;
  Camera cam;
  float turnVelocity;

  void Start() {
    controller = GetComponent<CharacterController>();
    cam = Camera.main;
  }

  void Update() {
    float horiz = Input.GetAxisRaw("Horizontal");
    float vert = Input.GetAxisRaw("Vertical");

    Vector3 dir = new Vector3(horiz, 0, vert).normalized;

    if(dir.magnitude >= 0.1f){
      float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
      float angle =Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSpeed);
      transform.rotation = Quaternion.Euler(0, angle, 0);

      Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
      controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }
  }
}
