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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //jetpack
        if (Input.GetButtonDown("Jump") && enoughTimeinAir && jetpackSlider.value >= 4 && jetpackCooldown > 0.2f)
        {
            usingJetpack = true;
            jetpackCooldown = 0;

            //reset gravity
            GetComponent<PlayerMovement>().gravity = 0;

            print("jetpack");
            //jetpack energy loss
            jetpackSlider.value -= 4;

            //jetpack force
            GetComponent<Rigidbody>().AddForce(transform.up * jetpackForce * Time.deltaTime);
        }

        else
        {
            usingJetpack = false;
        }

        //cooldown jetpack
        jetpackCooldown += Time.deltaTime;
        if (jetpackCooldown > 1)
        {
            jetpackSlider.value += Time.deltaTime * 4;
        }

        //jetpack only when long time in air
        if(GetComponent<PlayerMovement>().isGrounded == false)
        {
            timeInAir += Time.deltaTime;
            if(timeInAir > 0.2f)
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
}
