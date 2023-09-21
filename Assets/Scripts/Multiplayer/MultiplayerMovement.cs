using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MultiplayerMovement : NetworkBehaviour
{
    [Header("Player")]
    public Rigidbody r;

    [Header("Movement")]
    public float speed;
    private Vector3 movement;

    public float forwardSpeed;
    public float sideSpeed;

    public float beginningSpeed;
    public float maxSpeed;
    private float normalDrag;
    public float dragWhenPlayerNotMoving;

    [Header("jetpackMovement")]
    public float jetpackSpeed;

    [Header("Jumping")]
    public float gravity;
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
            if (isGrounded)
            {
                if (GetComponent<PlayerSliding>().isSliding == false)
                {
                    forwardSpeed = Input.GetAxis("Vertical");
                }

                else
                {
                    forwardSpeed = 1;
                }


                if (GetComponent<PlayerSliding>().isSliding == false)
                {
                    sideSpeed = Input.GetAxis("Horizontal");
                }
            }

            //player can look around when jumping
            if (isGrounded)
            {
                // Calculate the new velocity based on input
                movement = transform.forward * forwardSpeed * speed + transform.right * sideSpeed * speed;
            }

            //air movement speed

            if (GetComponent<Jetpack>().usingJetpack)
            {
                forwardSpeed = Input.GetAxis("Vertical");
                sideSpeed = Input.GetAxis("Horizontal");

                // Calculate the new velocity based on input
                movement = transform.forward * forwardSpeed * jetpackSpeed + transform.right * sideSpeed * jetpackSpeed;
            }

            // Set the Rigidbody's velocity directly
            r.velocity = new Vector3(movement.x, r.velocity.y, movement.z);

            //add drag when player not moving
            if (sideSpeed < 0.6f && forwardSpeed < 0.6f && isGrounded)
            {
                r.drag = dragWhenPlayerNotMoving;
            }

            else
            {
                r.drag = normalDrag;
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.isLocalPlayer)
        {
            //limit speed
            if (r.velocity.magnitude > maxSpeed && GetComponent<PlayerSliding>().isSliding == false)
            {
                GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(r.velocity, maxSpeed);
            }

            //change gravity
            if (GetComponent<Jetpack>().usingJetpack == false)
            {
                r.AddForce(-transform.up * gravity * Time.deltaTime);
                if (GetComponent<Jetpack>().activateJetpackGravity == false)
                {
                    if (isGrounded == false)
                    {
                        gravity += timeInAirGravity;

                        //als te lang in de lucht komt er meer gravity
                        gravity = gravity * 1.03f;
                    }

                    else
                    {
                        gravity = normalGravity;
                    }
                }

            }


            //jumping
            if (Input.GetButton("Jump") && isGrounded)
            {
                r.AddForce(transform.up * jumpSpeed * Time.deltaTime);
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
