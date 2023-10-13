using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBarrel : Barrel
{
    public Transform[] barrelPositions;
    private RaycastHit hit;

    [Header("Rotation")]
    public Transform gatlingGun;
    public float rotationSpeed;
    public bool rotateGatlingGun;
    private Quaternion targetRotation;

    private float rotationTimeCount;
    public float maxRotationTime;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (rotateGatlingGun)
        {
            // Define the angle by which you want to rotate the gatlingGun barrel.
            float rotationAmount = 30f; // You can adjust this value to control the rotation angle.

            // Calculate the new target rotation based on the current rotation.
            Quaternion targetRotation = gatlingGun.localRotation * Quaternion.Euler(0, 0, rotationAmount);

            // Interpolate between the current rotation and the target rotation.
            gatlingGun.localRotation = Quaternion.Slerp(gatlingGun.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Update the rotationTimeCount.
            rotationTimeCount += Time.deltaTime;

            // Check if the rotation time has exceeded the maximum allowed rotation time.
            if (rotationTimeCount > maxRotationTime)
            {
                rotateGatlingGun = false;
                rotationTimeCount = 0;
            }
        }

    }

    public override void ShootBullet()
    {
        print("multiBarrel");
        rotateGatlingGun = true;

        if (usingShrapnel)
        {
            for (int i = 0; i < barrelPositions.Length; i++)
            {
                float numBullets = Random.Range(7, 15);
                for (int i2 = 0; i2 < numBullets; i2++)
                {
                    // Instantiate the bullet with the correct initial rotation
                    GameObject prefabBullet = Instantiate(bulletType, barrelPositions[i].position, Quaternion.identity);

                    Quaternion spreadRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(-20, 20), 0f);

                    //create a raycast direction from the spread rotation
                    Vector3 rayDirection = spreadRotation * cam.forward;

                    prefabBullet.transform.LookAt(prefabBullet.transform.position + rayDirection);
                    prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * bulletForce * Time.deltaTime);
                }
            }

        }

        else
        {
            for (int i = 0; i < barrelPositions.Length; i++)
            {
                GameObject bulletPrefab = Instantiate(bulletType, barrelPositions[i].position, Quaternion.identity);
                bulletPrefab.GetComponent<Rigidbody>().AddForce(cam.forward * bulletForce * Time.deltaTime);
            }
        }

    }

    public override void Shoot()
    {
        print("multiBarrel");
        base.Shoot();
        rotateGatlingGun = true;

        //raycast version
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);
        if(usingShrapnel == false)
        {
            if (hit.transform != null)
            {
                GameObject prefabRaycast = Instantiate(particleRaycast, hit.point, Quaternion.identity);
                Destroy(prefabRaycast, 1);
                if (hit.transform.gameObject.tag == "Player")
                {
                    for (int i = 0; i < barrelPositions.Length; i++)
                    {
                        //damage multiplies with each chamber
                        hit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                    }

                }
            }
        }

        else
        {
            float numBullets = Random.Range(7, 15);
            for (int i = 0; i < numBullets; i++)
            {
                //calculate a random rotation within the specified spread angle
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                //perform the raycast
                RaycastHit multiBarrelHit;
                if (Physics.Raycast(barrelPosition.position, rayDirection, out multiBarrelHit, 100))
                {
                    GameObject prefabRaycast = Instantiate(particleRaycast, multiBarrelHit.point, Quaternion.identity);
                    Destroy(prefabRaycast, 1);
                    Debug.DrawLine(cam.position, multiBarrelHit.point, Color.red, 0.1f);

                    //raycast bullet
                    if (multiBarrelHit.transform.gameObject.tag == "Player")
                    {
                        multiBarrelHit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                    }
                }

                else
                {
                    // Handle a miss
                }

            }
        }
        
    }
}
