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
    }

    void RifleGun()
    {
        //raycast
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);


        if (ammo > 0 && schootingResetTime > schootingCooldownMaxTime)
        {
            //AUTOMATIC BARREL AND SINGLE BARREL
            if (Input.GetButton("Fire1") && GetComponent<Chamber>().usingSingleShot || Input.GetButton("Fire1") && GetComponent<Chamber>().usingAutoChamber)
            {
                Fire();
            }

            //TAB BARREL
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
