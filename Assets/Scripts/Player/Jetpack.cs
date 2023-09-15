using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    public Slider jetpackSlider;
    public float jetpackForce;
    public float beginningJetpackForce;
    public bool usingJetpack;
    public float jetpackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        beginningJetpackForce = jetpackForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        jetpackForce = Mathf.Clamp(jetpackForce, 0, 2000);
        if (Input.GetButton("Jump") && GetComponent<PlayerMovement>().isGrounded == false && jetpackSlider.value != 0)
        {
            jetpackCooldown = 0;
            usingJetpack = true;
            jetpackSlider.value -= Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(transform.up * jetpackForce * Time.deltaTime);
            jetpackForce = jetpackForce * 1.07f;

        }

        else
        {
            usingJetpack = false;
            jetpackForce = beginningJetpackForce;
            jetpackCooldown += Time.deltaTime;
            if(jetpackCooldown > 4)
            {
                jetpackSlider.value += Time.deltaTime;
            }
        }
    }
}
