using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    public bool usingRevolver;
    public bool usingSingleShot;
    public bool usingAutoChamber;

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

    }

    void SingleShot()
    {

    }

    void AutoChamber()
    {

    }
}
