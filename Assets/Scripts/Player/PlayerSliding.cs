using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliding : MonoBehaviour
{

    public Slider slider;
    public float amountOfSlowingDown;
    public float slidingTime;
    private float beginningSlidingTime;

    // Start is called before the first frame update
    void Start()
    {
        beginningSlidingTime = slidingTime;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = slidingTime;
        if(Input.GetKey(KeyCode.LeftShift) && slidingTime > 0)
        {
            //sliding
            slidingTime -= Time.deltaTime;
            GetComponent<PlayerMovement>().speed -= amountOfSlowingDown;
        }

        else
        {
            //reset sliding time
            GetComponent<PlayerMovement>().speed = GetComponent<PlayerMovement>().beginningSpeed;
            if (slidingTime < beginningSlidingTime)
            {
                slidingTime += Time.deltaTime;
            }

            else
            {
                slidingTime = beginningSlidingTime;
            }
        }
    }

    private void FixedUpdate()
    {
        //speed value needs to stay positive
        Mathf.Clamp(GetComponent<PlayerMovement>().speed, 0, GetComponent<PlayerMovement>().beginningSpeed);
    }
}
