using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 6f;
  public float turnSpeed = .1f;
  public float gravity = -20f;
  public float jumpHeight = 2f;

  public Transform groundCheck;
  public float groundDist = 0.4f;
  public LayerMask groundMask;

  CharacterController controller;
  Camera cam;
  float turnVelocity;
  Vector3 grav;
  bool onGround;

  void Start() {
    controller = GetComponent<CharacterController>();
    cam = Camera.main;

    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update() {
    onGround = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

    if(onGround && grav.y < 0)
      grav.y = -2f; // Small amount helps keep player on ground
    
    float x = Input.GetAxisRaw("Horizontal");
    float z = Input.GetAxisRaw("Vertical");

    Vector3 dir = new Vector3(x, 0, z).normalized;

    if(dir.magnitude >= 0.1f){
      float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
      float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSpeed);
      transform.rotation = Quaternion.Euler(0, angle, 0);

      Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
      controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    if (Input.GetButtonDown("Jump") && onGround)
      grav.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

    grav.y += gravity * Time.deltaTime;
    controller.Move(grav * Time.deltaTime);
  }
}
