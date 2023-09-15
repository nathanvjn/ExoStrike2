using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MultiplayerMovement : NetworkBehaviour
{
    [Header("Movement")]
    public float speed;

    public float forwardSpeed;
    public float sideSpeed;

    public float beginningSpeed;
    public float maxSpeed;
    private float normalDrag;
    public float dragWhenPlayerNotMoving;

    [Header("Jumping")]
    public float gravity;
    public Transform bottomPlayer;
    public RaycastHit groundHit;
    public bool isGrounded;
    public float jumpSpeed;
    public float timeInAirGravity;
    private float normalGravity;

    private void Start()
    {
        beginningSpeed = speed;
        normalDrag = GetComponent<Rigidbody>().drag;
        normalGravity = gravity;
    }

    void Update()
    {
        if (this.isLocalPlayer)
        {
            //movement
            forwardSpeed = Input.GetAxis("Vertical");

            if (GetComponent<PlayerSliding>().isSliding == false)
            {
                sideSpeed = Input.GetAxis("Horizontal");
            }

            // Calculate the new velocity based on input
            Vector3 movement = transform.forward * forwardSpeed * speed + transform.right * sideSpeed * speed;

            // Set the Rigidbody's velocity directly
            GetComponent<Rigidbody>().velocity = new Vector3(movement.x, GetComponent<Rigidbody>().velocity.y, movement.z);

            //add drag when player not moving
            if (sideSpeed < 0.6f && forwardSpeed < 0.6f && isGrounded)
            {
                GetComponent<Rigidbody>().drag = dragWhenPlayerNotMoving;
            }

            else
            {
                GetComponent<Rigidbody>().drag = normalDrag;
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.isLocalPlayer)
        {
            //limit speed
            if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed && GetComponent<PlayerSliding>().isSliding == false)
            {
                GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, maxSpeed);
            }

            //change gravity
            GetComponent<Rigidbody>().AddForce(-transform.up * gravity * Time.deltaTime);
            if (isGrounded == false)
            {
                gravity += timeInAirGravity;
            }

            else
            {
                gravity = normalGravity;
            }

            //jumping
            if (Input.GetButton("Jump") && isGrounded)
            {
                GetComponent<Rigidbody>().AddForce(transform.up * jumpSpeed * Time.deltaTime);
                print("jumping");
            }
        }
    }

    //isGrounded
    private void OnTriggerEnter(Collider other)
    {
        if (this.isLocalPlayer)
        {
            //player does not get forced into ground
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0f, GetComponent<Rigidbody>().velocity.z);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
