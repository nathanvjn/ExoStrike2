using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float gravity;
    private float normalDrag;
    public float dragWhenPlayerNotMoving;
    private void Start()
    {
        normalDrag = GetComponent<Rigidbody>().drag;
    }

    void Update()
    {
        //movement
        float forwardSpeed = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().AddForce(transform.forward * forwardSpeed * speed);

        float sideSpeed = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().AddForce(transform.right * sideSpeed * speed);

        //add drag when player not moving
        if(sideSpeed < 0.6f && forwardSpeed < 0.6f)
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
        GetComponent<Rigidbody>().AddForce(-transform.up * gravity);
    }
}
