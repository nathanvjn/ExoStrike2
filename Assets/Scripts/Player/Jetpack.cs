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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //jetpack
        if (Input.GetButtonDown("Jump") && GetComponent<PlayerMovement>().isGrounded == false && jetpackSlider.value >= 4)
        {
            usingJetpack = true;
            jetpackCooldown = 0;

            //reset gravity
            GetComponent<PlayerMovement>().gravity = 0;

            //jetpack energy loss
            jetpackSlider.value -= 4;

            //jetpack force
            GetComponent<Rigidbody>().AddForce(transform.up * jetpackForce * Time.deltaTime);
        }

        else
        {
            usingJetpack = false;
        }

        jetpackCooldown += Time.deltaTime;
        if (jetpackCooldown > 1)
        {
            jetpackSlider.value += Time.deltaTime * 2;
        }
    }
}
