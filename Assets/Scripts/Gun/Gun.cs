using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{

    [Header("Aiming")]
    private RaycastHit hit;
    public Transform cam;
    public Transform chamberOfGun;

    [Header("Schooting")]
    public float ammo;

    //schooting cooldown
    public float schootingCooldownMaxTime;
    private float schootingResetTime;

    //bullet
    public GameObject bullet;
    public float bulletSpeed;

    //player
    public GameObject player;

    [Header("UI")]
    
    public TextMeshProUGUI ammoText;
    public GameObject particleMuzzle;
    public GameObject particleGrenade;
    public Transform particlePosition;
    public Transform particleGrenadePosition;

    

    
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
        //raycast
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);


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


        //SCHOOTING PARTICLES
        if(GetComponent<Mag>().usingGrenadeMag == false)
        {
            //particle for schooting bullet
            GameObject prefab = Instantiate(particleMuzzle, particlePosition.position, Quaternion.identity);
            prefab.transform.parent = particlePosition;
            prefab.transform.rotation = particlePosition.rotation;
            StartCoroutine(ScaleParticlesOverTime());

            IEnumerator ScaleParticlesOverTime()
            {
                float elapsedTime = 0f;
                float scalingDuration = 0.1f; //adjust the duration as needed

                while (elapsedTime < scalingDuration)
                {
                    float scale = Mathf.Lerp(0f, 0.1f, elapsedTime / scalingDuration);
                    prefab.transform.localScale = new Vector3(scale, scale, scale); //set the particle size

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }


            }

            Destroy(prefab, 0.11f);
        }

        else
        {
            //particle for schooting grenade
            GameObject grenadePrefab = Instantiate(particleGrenade, particlePosition.position, Quaternion.identity);
            grenadePrefab.transform.parent = particlePosition;
            grenadePrefab.transform.rotation = particleGrenadePosition.rotation;
            Destroy(grenadePrefab, 1);
        }
        

        




        //if big barrel
        if (GetComponent<Barrel>().usingBigBarrel)
        {
            for (int i = 0; i < GetComponent<Barrel>().numBullets; i++)
            {
                //calculate a random rotation within the specified spread angle
                float spreadAngle = GetComponent<Barrel>().spreadAngle;
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                //perform the raycast
                RaycastHit bigBarrelHit;
                if(GetComponent<Mag>().usingNormalMag)
                {
                    if (Physics.Raycast(cam.position, rayDirection, out bigBarrelHit, GetComponent<Barrel>().maxRange))
                    {

                        Debug.DrawLine(cam.position, bigBarrelHit.point, Color.red, 0.1f);

                        //raycast bullet
                        if (bigBarrelHit.transform.gameObject.tag == "Player")
                        {
                            bigBarrelHit.transform.gameObject.GetComponent<Health>().playerHealth -= GetComponent<Barrel>().bigBarrelDamage;
                        }
                    }

                    else
                    {
                        // Handle a miss
                        Debug.DrawRay(cam.position, rayDirection * GetComponent<Barrel>().maxRange, Color.green, 0.1f);
                    }
                }

                //explosive bullet
                else if (GetComponent<Mag>().usingGrenadeMag)
                {
                    GameObject prefabBullet = Instantiate(bullet, chamberOfGun.position, Quaternion.identity);
                    prefabBullet.GetComponent<Bullet>().player = player;
                    prefabBullet.transform.LookAt(prefabBullet.transform.position + rayDirection);
                    prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * bulletSpeed * Time.deltaTime);
                }

            }
        }

        //if normal and double barrel
        else if (hit.transform != null && GetComponent<Mag>().usingGrenadeMag == false)
        {
            //player gets damage
            if (hit.transform.gameObject.tag == "Player")
            {
                print("hittingEnemy");

                //damage if normal barrel
                if (GetComponent<Barrel>().usingNormalBarrel)
                {
                    if(GetComponent<Mag>().usingNormalMag)
                    {
                        hit.transform.gameObject.GetComponent<Health>().playerHealth -= GetComponent<Barrel>().normalBarrelDamage;
                    }
                }

                //damage if double barrel
                else if(GetComponent<Barrel>().usingMultiBarrel)
                {
                    print("multibarrel");
                    if(GetComponent<Mag>().usingNormalMag)
                    {
                        //damage multiplies by barrels
                        print("work");
                        hit.transform.gameObject.GetComponent<Health>().playerHealth -= GetComponent<Barrel>().normalBarrelDamage * GetComponent<Barrel>().amountOfBarrels;
                    }

                }
            }
        }


        //if grenade mag with normal and double
        else if (GetComponent<Mag>().usingGrenadeMag)
        {
            if(GetComponent<Barrel>().usingNormalBarrel)
            {
                GameObject prefabBullet = Instantiate(bullet, chamberOfGun.position, Quaternion.identity);
                prefabBullet.GetComponent<Bullet>().player = player;
                prefabBullet.GetComponent<Rigidbody>().AddForce(cam.forward * bulletSpeed * Time.deltaTime);
                prefabBullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            else if(GetComponent<Barrel>().usingMultiBarrel)
            {
                //more chambers, more bullets

                float amountOfBarrels = GetComponent<Barrel>().amountOfBarrels;
                if (amountOfBarrels == 2)
                {
                    for (int i = 0; i < GetComponent<Barrel>().twoBarrels.Length; i++)
                    {
                        GameObject prefabBullet = Instantiate(bullet, GetComponent<Barrel>().twoBarrels[i].position, Quaternion.identity);
                        prefabBullet.GetComponent<Bullet>().player = player;
                        prefabBullet.GetComponent<Rigidbody>().AddForce(cam.forward * bulletSpeed * Time.deltaTime);
                        prefabBullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        print("2");
                    }
                }

                else if (amountOfBarrels == 3)
                {
                    for (int i = 0; i < GetComponent<Barrel>().tripleBarrels.Length; i++)
                    {
                        GameObject prefabBullet = Instantiate(bullet, GetComponent<Barrel>().tripleBarrels[i].position, Quaternion.identity);
                        prefabBullet.GetComponent<Bullet>().player = player;
                        prefabBullet.GetComponent<Rigidbody>().AddForce(cam.forward * bulletSpeed * Time.deltaTime);
                        prefabBullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        print("3");
                    }
                }

                else if (amountOfBarrels == 4)
                {
                    for (int i = 0; i < GetComponent<Barrel>().fourBarrels.Length; i++)
                    {
                        GameObject prefabBullet = Instantiate(bullet, GetComponent<Barrel>().fourBarrels[i].position, Quaternion.identity);
                        prefabBullet.GetComponent<Bullet>().player = player;
                        prefabBullet.GetComponent<Rigidbody>().AddForce(cam.forward * bulletSpeed * Time.deltaTime);
                        prefabBullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        print("4");
                    }
                }

                else if (amountOfBarrels == 5)
                {
                    for (int i = 0; i < GetComponent<Barrel>().fiveBarrels.Length; i++)
                    {
                        GameObject prefabBullet = Instantiate(bullet, GetComponent<Barrel>().fiveBarrels[i].position, Quaternion.identity);
                        prefabBullet.GetComponent<Bullet>().player = player;
                        prefabBullet.GetComponent<Rigidbody>().AddForce(cam.forward * bulletSpeed * Time.deltaTime);
                        prefabBullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        print("5");
                    }
                }
            }
            
        }

        



    }
}
