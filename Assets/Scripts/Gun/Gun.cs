using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private RaycastHit hit;
    public Transform cam;

    public string gunType;
    public float ammo;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check gun type
        if(gunType == "Rifle")
        {
            RifleGun();
        }

        if(gunType == "Pistol")
        {
            PistolGun();
        }
    }

    void RifleGun()
    {
        //raycast
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);

        if(ammo > 0)
        {
            
            if (Input.GetButtonDown("Fire1"))
            {
                //player fires
                ammo -= 1;

                if (hit.transform != null)
                {
                    //player gets damage
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        print("hittingEnemy");
                    }
                }
            }
        }

        else
        {
            //no amo
        }
        
    }

    void PistolGun()
    {

    }
}
