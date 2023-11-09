using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJump : MonoBehaviour
{
    public Animator robotJump;

    public float jumpHeight;
    public float movementSpeed;
    public float timeBeforeDestroy;
    private float lifeTime;
    public float timeWhenJumping;
    public float timeWhenLanding;
    private bool didJump;
    private void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        Destroy(gameObject, timeBeforeDestroy);

        lifeTime += Time.deltaTime;
        if(lifeTime > timeWhenJumping)
        {
            robotJump.SetBool("goingToJump", true);
            if(didJump == false)
            {
                GetComponent<Rigidbody>().AddForce(transform.up * jumpHeight * Time.deltaTime);
                didJump = true;
            }

            if(lifeTime > timeWhenLanding)
            {
                //check if landing
                print("hoi");
                robotJump.SetBool("goingToLand", true);
            }
        }
    }
}
