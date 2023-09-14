using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliding : MonoBehaviour
{
    public bool isSliding;
    public float amountOfSlowingDown;
    public float slidingSpeed;
    private float slidingBeginSpeed;

    public Transform camPosition;
    public Transform camSlidingPosition;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        slidingBeginSpeed = slidingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
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
