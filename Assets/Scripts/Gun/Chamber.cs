using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    //different chambers
    public bool usingRevolver;
    public bool usingSingleShot;
    public bool usingAutoChamber;

    [Header("autoChamber")]
    public float gunCooldown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(usingRevolver)
        {
            Revolver();
        }

        else if(usingSingleShot)
        {
            SingleShot();
        }

        else if(usingAutoChamber)
        {
            AutoChamber();
        }


    }

    void Revolver()
    {
        //not autoChamber
        GetComponent<Gun>().schootingCooldownMaxTime = 0;
    }

    void SingleShot()
    {
        //not autoChamber
        GetComponent<Gun>().schootingCooldownMaxTime = 0;
    }

    void AutoChamber()
    {
        //autoChamber has a short cooldown
        GetComponent<Gun>().schootingCooldownMaxTime = gunCooldown;
    }
}
