using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
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
        normalDrag = GetComponent<Rigidbody>().drag;
        normalGravity = gravity;
    }

    void Update()
    {
        //movement
        float forwardSpeed = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().AddForce(transform.forward * forwardSpeed * speed);

        float sideSpeed = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().AddForce(transform.right * sideSpeed * speed);

        //add drag when player not moving
        if(sideSpeed < 0.6f && forwardSpeed < 0.6f && isGrounded)
        {
            GetComponent<Rigidbody>().drag = dragWhenPlayerNotMoving;
        }

        else
        {
            GetComponent<Rigidbody>().drag = normalDrag;
        }

    }

    private void FixedUpdate()
    {
        //limit speed
        if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, maxSpeed);
        }

        //change gravity
        GetComponent<Rigidbody>().AddForce(-transform.up * gravity * Time.deltaTime);
        if(isGrounded == false)
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

    //isGrounded
    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
