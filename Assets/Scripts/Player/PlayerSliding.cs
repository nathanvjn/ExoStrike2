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

    //check distance to ground
    public RaycastHit hit;
    public float hitDistance;

    //move cam position
    public Transform camPosition;
    public Transform camSlidingPosition;
    public Transform cam;

    //test
    public Vector3 velocity;
    public float magnitude;
    //test


    //sliding physics code
    public float slideForce = 10f;
    public float maxSlideAngle = 45f;

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
        if(r.velocity.y > 30)
        {
            r.velocity = new Vector3(r.velocity.x, 30, r.velocity.z);
        }

        //sliding
        Physics.Raycast(transform.position, -transform.up, out hit, 7);
        if(Input.GetKey(KeyCode.LeftShift) && hit.distance < hitDistance)
        {
            //sliding
            isSliding = true;

            //sliding speed is the speed the player begins with when sliding
            GetComponent<PlayerMovement>().speed = slidingSpeed;

            if (GetComponent<PlayerMovement>().speed > 0)
            {
                //cam position (sliding effect)
                cam.position = camSlidingPosition.position;
                
                //slowing down
                slidingSpeed -= amountOfSlowingDown;
            }

            //sliding off hills happens when no more force
            if(slidingSpeed < 20)
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
                        r.AddForce(slideDirection * slideForce, ForceMode.Force);
                    }
                }
            }
            
            
        }

        else
        {
            //no more sliding
            isSliding = false;

            slidingSpeed = slidingBeginSpeed;
            GetComponent<PlayerMovement>().speed = GetComponent<PlayerMovement>().beginningSpeed;
            //cam position (normal walking)
            cam.position = camPosition.position;
        }
    }

    private void FixedUpdate()
    {
        //speed value needs to stay positive
        Mathf.Clamp(GetComponent<PlayerMovement>().speed, 0, GetComponent<PlayerMovement>().beginningSpeed);
    }
}
