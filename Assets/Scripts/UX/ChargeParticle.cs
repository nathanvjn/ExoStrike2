using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeParticle : MonoBehaviour
{
    public Chamber chamber;
    public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float chargeTime = chamber.chargeTime;
        float maxChargeTime = chamber.maxChargeTime;

        if(chamber.usingChargeParticle && chamber.usingCharge)
        {
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LineRenderer>().positionCount = 2;

            Vector3 startPoint = gun.barrel.barrelPosition.position;
            Vector3 endPoint = gun.barrel.barrelPosition.position + transform.forward * (chargeTime * 4 / maxChargeTime);

            GetComponent<LineRenderer>().SetPosition(0, startPoint);
            GetComponent<LineRenderer>().SetPosition(1, endPoint);
        }

        else
        {
            GetComponent<LineRenderer>().enabled = false;
        }
    }
}
