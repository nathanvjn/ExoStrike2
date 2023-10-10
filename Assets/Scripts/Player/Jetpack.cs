using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    public Slider jetpackSlider;
    public float jetpackForce;
    private float jetpackCooldown;
    public bool usingJetpack;

    private float timeInAir;
    private bool enoughTimeinAir;

    private void Update()
    {

        //cooldown jetpack
        jetpackCooldown += Time.deltaTime;
        if (jetpackCooldown > 1)
        {
            jetpackSlider.value += Time.deltaTime * 4;
        }

        //jetpack only when long time in air
        if (GetComponent<PlayerMovement>().isGrounded == false)
        {
            timeInAir += Time.deltaTime;
            if (timeInAir > 0.2f)
            {
                enoughTimeinAir = true;
            }
        }

        else
        {
            timeInAir = 0;
            enoughTimeinAir = false;
        }
    }

    void FixedUpdate()
    {

        //jetpack
        if (Input.GetButton("Jump") && enoughTimeinAir && jetpackSlider.value >= 4 && jetpackCooldown > 0.2f)
        {
            jetpackCooldown = 0;
            usingJetpack = true;

            //reset gravity
            GetComponent<PlayerMovement>().gravity = 0;
            GetComponent<Rigidbody>().AddForce(transform.up * jetpackForce * Time.deltaTime);

            print("jetpack");
            //jetpack energy loss
            jetpackSlider.value -= 4;


        }

        else
        {
            usingJetpack = false;
        }
    }
}
