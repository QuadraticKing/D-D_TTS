using System;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed;
    public float playerDistance;
    public Vector3 previousPosition;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;
    public Transform angleFace;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    bool onGround;

    public Transform direction;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.freezeRotation = true;

        groundDrag = gameObject.transform.localScale.y;
        playerHeight = gameObject.transform.localScale.y * 2;
    }

    private void Update()
    {
        //Check if Grounded
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        MyInput();

        //Handle Ground Drag
        if (onGround)
            Rigidbody.linearDamping = groundDrag;
        else
            Rigidbody.linearDamping = 0;

        SpeedControl();

        //Calculate Distance
        playerDistance += Vector3.Distance(previousPosition, transform.position)/7;
        previousPosition = transform.position;


        //Debug Section
        Debug.Log("On Ground = " + onGround);
        Debug.Log("Distance Travelled = " + playerDistance);
    }

    private void FixedUpdate()
    {
        AimPlayer();
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //When to Jump
        if(Input.GetKey(jumpKey) && readyToJump == true && onGround == true)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //Calculate movement direction
        moveDirection = (direction.forward * verticalInput) + (direction.right * horizontalInput);
        
        if (onGround)
        {
            Rigidbody.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);
        } else if (onGround == false) {
            Rigidbody.AddForce(moveDirection.normalized * playerSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void AimPlayer()
    {
        transform.LookAt(angleFace);
    }
    private void SpeedControl()
    {
        Vector3 velocity = new Vector3(Rigidbody.linearVelocity.x, 0, Rigidbody.linearVelocity.z);

        if(velocity.magnitude > playerSpeed)
        {
            Vector3 velocityLimit = velocity.normalized * playerSpeed;
            Rigidbody.linearVelocity = new Vector3(velocityLimit.x, Rigidbody.linearVelocity.y, velocityLimit.z);
        }
    }

    private void Jump()
    {
        //Reset y Velocity
        Rigidbody.linearVelocity = new Vector3(Rigidbody.linearVelocity.x, 0, Rigidbody.linearVelocity.z);

        Rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
