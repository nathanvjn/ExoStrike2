using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    public Rigidbody r;

    [Header("Movement")]
    public float speed;
    private Vector3 movement;
    private Vector3 airMovement;

    public float forwardSpeed;
    public float sideSpeed;

    public float beginningSpeed;
    public float maxSpeed;
    private float normalDrag;
    public float dragWhenPlayerNotMoving;

    [Header("Air Movement")]
    public float airSpeed;

    [Header("Jumping")]
    public float gravity;
    public RaycastHit groundHit;
    public bool isGrounded;
    public float jumpSpeed;
    public float timeInAirGravity;
    private float normalGravity;
    public float maxGravity;

    [Header("Gun")]
    public GameObject gun; //access player gun with onTrigger/onCollision
    public Animator gunAnimator;
    private float gunSwayTime;

    [Header("Sound Manager")]
    public SoundManager soundManager;

    private void Start()
    {
        beginningSpeed = speed;
        normalDrag = r.drag;
        normalGravity = gravity;
    }

    void Update()
    {
        //movement
        if(isGrounded)
        {
            if(GetComponent<PlayerSliding>().isSliding == false)
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

        //player can only move when on ground
        if(isGrounded)
        {
            // Calculate the new velocity based on input
            movement = transform.forward * forwardSpeed * speed + transform.right * sideSpeed * speed;
            gunSwayTime = 0;
        }

        //air movement speed
        if(isGrounded == false)
        {
            sideSpeed = Input.GetAxis("Horizontal");

            // Calculate the new velocity based on input
            airMovement = transform.right * sideSpeed * airSpeed;
            r.AddForce(airMovement);

            gunSwayTime += Time.deltaTime;
            if(gunSwayTime > 0.2f)
            {
                gunAnimator.SetBool("gunSway", false);
                gunSwayTime = 0;
            }
        }

        // Set the Rigidbody's velocity directly
        r.velocity = new Vector3(movement.x, r.velocity.y, movement.z);

        //add drag when player not moving
        if (sideSpeed < 0.6f && forwardSpeed < 0.6f && isGrounded)
        {
            r.drag = dragWhenPlayerNotMoving;
            gunAnimator.SetBool("gunSway", false);
        }

        else
        {
            r.drag = normalDrag;
            if(isGrounded)
            {
                gunAnimator.SetBool("gunSway", true);
            }
        }

    }

    private void FixedUpdate()
    {
        //limit jump gravity
        gravity = Mathf.Clamp(gravity, 0, maxGravity);

        //limit speed
        if (r.velocity.magnitude > maxSpeed && GetComponent<PlayerSliding>().isSliding == false)
        {
            GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(r.velocity, maxSpeed);
        }

        //change gravity
        
        r.AddForce(-transform.up * gravity * Time.deltaTime);
            
        if (isGrounded == false)
        {
            gravity += timeInAirGravity;

            //als te lang in de lucht komt er meer gravity
            gravity = gravity * 1.05f;
        }

        else
        {
            gravity = normalGravity;
        }
            
            
        
        

        //jumping
        if (Input.GetButton("Jump") && isGrounded)
        {
            r.AddForce(transform.up * jumpSpeed * Time.deltaTime);
            print("jumping");

            //sound
            soundManager.JumpingSound();
        }
    }

    //isGrounded
    private void OnTriggerEnter(Collider other)
    {
        //player does not get forced into ground
        r.velocity = new Vector3(r.velocity.x, 0f, r.velocity.z);

        //sound
        soundManager.LandingSound();
        print("working");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag != "Pickup")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
   
}
