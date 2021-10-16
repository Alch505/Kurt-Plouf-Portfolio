using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRB : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.4f;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    [Header("Jumping")]
    public float jumpForce;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Ground Check")]
    float playerHeight = 2f;

    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Rigidbody rb;

    [Header("Grapple")]

    public Transform grapplePoint;
    Vector3 grappleDirection;

    public float grappleSpeedMultiplier;

    RaycastHit slopeHit;

    private bool OnSlope() 
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f)) 
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            Jump();
        }

        if (grapplePoint != null)
        {
            grappleDirection = grapplePoint.position - transform.position;
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput() 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ControlDrag() 
    {
        if (isGrounded)
        {
            if (rb.drag != groundDrag) 
            {
                StartCoroutine("BHOPTime");
            }
        }
        else 
        {
            rb.drag = airDrag;
        }
    }

    public IEnumerator BHOPTime() 
    {
        yield return new WaitForSeconds(0.2f);
        if (isGrounded) 
        {
            rb.drag = groundDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (grapplePoint != null) 
        {
            Grapple(grapplePoint, 3, moveSpeed * grappleSpeedMultiplier);
        }
    }

    void MovePlayer() 
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope()) 
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    void Grapple(Transform target, float distanceToStop, float speed)
    {
        rb.AddForce(grappleDirection.normalized * speed, ForceMode.Force);
    }
}
