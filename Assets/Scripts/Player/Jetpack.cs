using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    public Slider jetpackSlider;
    public float jetpackForce;
    private float beginningJetpackForce;
    public bool usingJetpack;
    private float jetpackCooldown;
    private float jetpackDelay;
    public float maxUpSpeed;
 



    // Start is called before the first frame update
    void Start()
    {
        beginningJetpackForce = jetpackForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //limit jetpack up speed
        jetpackForce = Mathf.Clamp(jetpackForce, 0, maxUpSpeed);

        //jetpack
        if (Input.GetButton("Jump") && GetComponent<PlayerMovement>().isGrounded == false && jetpackSlider.value != 0)
        {
            //wait 0.3 seconds before jetpack activates (issue with jumping & jetpack)
            jetpackDelay += Time.deltaTime;
            if(jetpackDelay > 0.3f)
            {
                jetpackCooldown = 0;
                usingJetpack = true;

                //jetpack energy loss
                jetpackSlider.value -= Time.deltaTime;

                //jetpack force
                GetComponent<Rigidbody>().AddForce(transform.up * jetpackForce * Time.deltaTime);
                jetpackForce = jetpackForce * 3f;
            }

        }

        else
        {
            usingJetpack = false;
            jetpackDelay = 0;
            jetpackForce = beginningJetpackForce;

            //jetpack regenerates energy after 4 seconds
            jetpackCooldown += Time.deltaTime;
            if(jetpackCooldown > 3)
            {
                jetpackSlider.value += Time.deltaTime;
            }
        }
    }
}
