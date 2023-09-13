using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float forwardSpeed = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().AddForce(transform.forward * forwardSpeed * speed);

        float sideSpeed = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().AddForce(transform.right * sideSpeed * speed);
        
    }
}
