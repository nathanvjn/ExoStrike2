using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliding : MonoBehaviour
{

    public Slider slider;

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
        }

        else
        {
            //reset sliding time
            if(slidingTime < beginningSlidingTime)
            {
                slidingTime += Time.deltaTime;
            }

            else
            {
                slidingTime = beginningSlidingTime;
            }
        }
    }
}
