using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    Vector3 velocity;
    Vector3 targetVelocity;

    public Transform grapplePoint;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        if (grapplePoint != null) 
        {
            MoveTowardsTarget(grapplePoint.position);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Gravity
        if (grapplePoint == null) 
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //Apply movement
        controller.Move(velocity * Time.deltaTime);
    }

    void MoveTowardsTarget(Vector3 target)
    {
        var offset = target - transform.position;

        if (offset.magnitude > .1f)
        {
            offset = offset.normalized * (speed * 2.5f);

            controller.Move(offset * Time.deltaTime);
        }
    }
}
