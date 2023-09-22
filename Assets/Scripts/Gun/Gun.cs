using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{

    [Header("Aiming")]
    private RaycastHit hit;
    public Transform cam;

    [Header("Schooting")]
    public string gunType;
    public float ammo;

    //schooting cooldown
    public float schootingCooldownMaxTime;
    private float schootingResetTime;

    //ui
    public TextMeshProUGUI ammoText;

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

        //ammo UI
        ammoText.text = ammo.ToString();
    }

    void RifleGun()
    {
        //raycast
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);

        if (ammo > 0 && schootingResetTime > schootingCooldownMaxTime)
        {
            
            if (Input.GetButton("Fire1"))
            {
                //player fires
                ammo -= 1;

                //time resets
                schootingResetTime = 0;

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

        //cooldown schooting
        schootingResetTime += Time.deltaTime;
        
    }

    void PistolGun()
    {

    }
}
