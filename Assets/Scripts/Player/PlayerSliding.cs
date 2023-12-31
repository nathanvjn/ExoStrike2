using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliding : MonoBehaviour
{
    //player
    public Rigidbody r;

    //standard sliding
    public bool isSliding;
    public float amountOfSlowingDown;
    public float slidingSpeed;
    private float slidingBeginSpeed;

    //move cam position
    public Transform camPosition;
    public Transform camSlidingPosition;
    public Transform cam;

    //test
    public Vector3 velocity;
    public float magnitude;
    //test


    //sliding physics code
    public float slideForce;
    public float maxSlideAngle;

    //gun
    public Animator gun;

    // Start is called before the first frame update
    void Start()
    {
        slidingBeginSpeed = slidingSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        //test
        velocity = r.velocity;
        magnitude = r.velocity.magnitude;
        //test

        //limit velocity when sliding and jumping
        if (r.velocity.y > 30)
        {
            r.velocity = new Vector3(r.velocity.x, 30, r.velocity.z);
        }

        //sliding
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sliding
            isSliding = true;
            gun.SetBool("sliding", true);

            //sliding speed is the speed the player begins with when sliding
            GetComponent<PlayerMovement>().speed = slidingSpeed;

            if (GetComponent<PlayerMovement>().speed > 0)
            {
                //cam position (sliding effect)
                cam.position = camSlidingPosition.position;

                //slowing down
                slidingSpeed -= amountOfSlowingDown;
            }

            if (GetComponent<PlayerMovement>().isGrounded)
            {
                RaycastHit hit;
                Vector3 playerBottom = transform.position - new Vector3(0f, GetComponent<CapsuleCollider>().height / 2f - GetComponent<CapsuleCollider>().radius, 0f);

                if (Physics.Raycast(playerBottom, Vector3.down, out hit))
                {
                    float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                    if (slopeAngle > maxSlideAngle)
                    {
                        // Calculate the slide direction based on the slope
                        Vector3 slideDirection = Vector3.Cross(Vector3.Cross(Vector3.up, hit.normal), hit.normal).normalized;

                        // Apply force opposite to the slope direction
                        r.AddForce(slideDirection * slideForce, ForceMode.Acceleration);
                    }
                }
            }


        }

        else
        {
            //no more sliding
            isSliding = false;
            gun.SetBool("sliding", false);

            slidingSpeed = slidingBeginSpeed;
            GetComponent<PlayerMovement>().speed = GetComponent<PlayerMovement>().beginningSpeed;
            //cam position (normal walking)
            cam.position = camPosition.position;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            gun.Play("SlidingBack", 0);
        }
    }

    private void FixedUpdate()
    {
        //speed value needs to stay positive
        Mathf.Clamp(GetComponent<PlayerMovement>().speed, 0, GetComponent<PlayerMovement>().beginningSpeed);
    }

}
