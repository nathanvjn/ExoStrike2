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
        RifleGun();

        //ammo UI
        ammoText.text = ammo.ToString();

        //shotgun cone will rotate with camera
        transform.position = cam.position;

        Quaternion cameraRotation = cam.rotation;
        cameraRotation = Quaternion.Euler(-cameraRotation.eulerAngles.x, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z);
        transform.rotation = cameraRotation;
    }

    void RifleGun()
    {
        //raycast normal barrel
        if(GetComponent<Barrel>().usingNormalBarrel)
        {
            Physics.Raycast(cam.position, cam.forward, out hit, 100);
            Debug.DrawLine(cam.position, hit.point, Color.red);
        }


        if (ammo > 0 && schootingResetTime > schootingCooldownMaxTime)
        {
            //AUTOMATIC CHAMBER AND SINGLE CHAMBER
            if (Input.GetButton("Fire1") && GetComponent<Chamber>().usingSingleShot || Input.GetButton("Fire1") && GetComponent<Chamber>().usingAutoChamber)
            {
                Fire();
            }

            //TAB CHAMBER
            else if (Input.GetButtonDown("Fire1") && GetComponent<Chamber>().usingRevolver)
            {
                Fire();
            }

        }

        else
        {
            //no amo
        }

        //cooldown schooting
        schootingResetTime += Time.deltaTime;
        
    }

    void Fire()
    {
        //player fires
        ammo -= 1;

        //time resets
        schootingResetTime = 0;




        //big barrel
        if (GetComponent<Barrel>().usingBigBarrel)
        {
            for (int i = 0; i < GetComponent<Barrel>().numBullets; i++)
            {
                // Calculate a random rotation within the specified spread angle
                float spreadAngle = GetComponent<Barrel>().spreadAngle;
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);

                // Create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                // Perform the raycast
                RaycastHit bigBarrelHit;

                if (Physics.Raycast(cam.position, rayDirection, out bigBarrelHit, GetComponent<Barrel>().maxRange))
                {
                    // Handle the hit (e.g., apply damage, spawn effects, etc.)
                    Debug.DrawLine(cam.position, bigBarrelHit.point, Color.red, 0.1f);
                }

                else
                {
                    // Handle a miss (e.g., spawn impact effects, etc.)
                    Debug.DrawRay(cam.position, rayDirection * GetComponent<Barrel>().maxRange, Color.green, 0.1f);
                }
            }
        }

        //normal and double barrel
        else if (hit.transform != null)
        {
            //player gets damage
            if (hit.transform.gameObject.tag == "Player")
            {
                print("hittingEnemy");

                //damage if normal barrel
                if (GetComponent<Barrel>().usingNormalBarrel)
                {
                    hit.transform.gameObject.GetComponent<Health>().playerHealth -= GetComponent<Barrel>().normalBarrelDamage;
                }
            }
        }

        
        
    }
}
